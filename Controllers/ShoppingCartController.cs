using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace project_mvc.Controllers;

public class ShoppingCartController : Controller
{
    private readonly ISnackRepository _snackRepository;
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public ShoppingCartController(ISnackRepository snackRepository, IShoppingCartRepository shoppingCartRepository)
    {
        _snackRepository = snackRepository;
        _shoppingCartRepository = shoppingCartRepository;
    }
    
    [Authorize]
    public IActionResult Index()
    {
        var items = _shoppingCartRepository.GetShoppingCartItems();
        
        var shoppingCartVM = new ShoppingCartViewModel{
            ShoppingCart = new ShoppingCart(items),
            ShoppingCartTotal = _shoppingCartRepository.GetShoppingCartTotal()
        };
        return View(shoppingCartVM);
    }

    [Authorize]
    public IActionResult AddItemToCart(int snackId){
        var snackFound = _snackRepository.Snacks.FirstOrDefault(s => s.SnackId == snackId);
        
        if(snackFound != null){
            _shoppingCartRepository.AddItemToCart(snackFound);
        }
        
        return RedirectToAction("Index");
    }

    [Authorize]
    public IActionResult RemoveItemFromCart(int snackId){
        var snackFound = _snackRepository.Snacks.FirstOrDefault(s => s.SnackId == snackId);

        if(snackFound != null){
            _shoppingCartRepository.RemoveFromCart(snackFound);
        }

        return RedirectToAction("Index");
    }

}

