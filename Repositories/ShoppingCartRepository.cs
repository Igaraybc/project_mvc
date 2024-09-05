using Microsoft.EntityFrameworkCore;

public class ShoppingCartRepository: IShoppingCartRepository
{
    private readonly AppDbContext _context;
    private readonly ShoppingCart _shoppingCart;

    public ShoppingCartRepository(AppDbContext context, ShoppingCart shoppingCart)
    {
        _context = context;
        _shoppingCart = shoppingCart;
    }

    public static ShoppingCart GetCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

        var context = services.GetService<AppDbContext>();

        string shoppingCartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

        session.SetString("CartId", shoppingCartId);

        return new ShoppingCart(context){
            ShoppingCartId = shoppingCartId
        };
    }


    public void AddItemToCart(Snack snack){
        var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(item => item.Snack.SnackId == snack.SnackId && item.ShoppingCartId == _shoppingCart.ShoppingCartId);

        if(shoppingCartItem == null){
            shoppingCartItem = new ShoppingCartItem{
                ShoppingCartId = _shoppingCart.ShoppingCartId,
                Snack = snack,
                Quantity = 1
            };
            _context.ShoppingCartItems.Add(shoppingCartItem);
        }
        else{
            shoppingCartItem.Quantity++;
        }
        _context.SaveChanges();
    }

    public int RemoveFromCart(Snack snack){
        var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(item => item.Snack.SnackId == snack.SnackId && item.ShoppingCartId == _shoppingCart.ShoppingCartId);

        var localQuantity = 0;

        if(shoppingCartItem != null){
            if(shoppingCartItem.Quantity > 1){
                shoppingCartItem.Quantity--;
                localQuantity = shoppingCartItem.Quantity;
            }
            else{
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }
        _context.SaveChanges();
        return localQuantity;
    }

    public List<ShoppingCartItem> GetShoppingCartItems(){
        return _shoppingCart.ShoppingCartItems ?? (_shoppingCart.ShoppingCartItems = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == _shoppingCart.ShoppingCartId).Include(s => s.Snack).ToList());
    }

    public void CleanCart(){
        var shoppingCartItems = _context.ShoppingCartItems.Where(cart => cart.ShoppingCartId == _shoppingCart.ShoppingCartId);

        _context.ShoppingCartItems.RemoveRange(shoppingCartItems);
        _context.SaveChanges();
    }

    public decimal GetShoppingCartTotal(){
        return _context.ShoppingCartItems.Where(c => c.ShoppingCartId == _shoppingCart.ShoppingCartId).Select(c => c.Snack.Price * c.Quantity).Sum();
    }    
}