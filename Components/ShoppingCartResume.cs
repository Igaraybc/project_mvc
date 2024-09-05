using Microsoft.AspNetCore.Mvc;

public class ShoppingCartResume: ViewComponent
{
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public ShoppingCartResume(IShoppingCartRepository shoppingCartRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
    }

    public IViewComponentResult Invoke(){
        var items = _shoppingCartRepository.GetShoppingCartItems();
       
        var shoppingCartVM = new ShoppingCartViewModel{
            ShoppingCart = new ShoppingCart(items),
            ShoppingCartTotal = _shoppingCartRepository.GetShoppingCartTotal()
        };
        return View(shoppingCartVM);
    }
}