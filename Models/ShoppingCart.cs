public class ShoppingCart
{ 

    public string ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems{ get; set; }

    public ShoppingCart(AppDbContext context)
    { }

    public ShoppingCart(List<ShoppingCartItem> items)
    {
        ShoppingCartItems = items;
    }
}