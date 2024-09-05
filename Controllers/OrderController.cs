using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class OrderController: Controller
{
    private readonly IOrderRepository _orderRepository;
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public OrderController(IShoppingCartRepository shoppingCartRepository, IOrderRepository orderRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _orderRepository = orderRepository;
    }

    [Authorize]
    public IActionResult Checkout(){
        return View();
    }

    public IActionResult CheckoutComplete(){
        return View();
    }

    [Authorize]
    [HttpPost]
    public IActionResult Checkout(Order order)
    {
        int totalOrderItems = 0;
        decimal totalOrderPrice = 0.0m;

        //obter carrinho de compras do cliente
        List<ShoppingCartItem> items = _shoppingCartRepository.GetShoppingCartItems();
        
        if(items.Count == 0){
            ModelState.AddModelError("", "Seu carrinho está vazio, que tal incluir um lanche...");
        }

        //Calcula quantidade de itens e o preço total do pedido
        foreach(var item in items){
            totalOrderItems += item.Quantity;
            totalOrderPrice += item.Quantity * item.Snack.Price;
        }

        //atribuir valores obtidos ao pedido
        order.TotalOrderItems = totalOrderItems;
        order.OrderTotal = totalOrderPrice;

        //Validar os dados do pedido
        if(ModelState.IsValid){
            //criar pedido
            _orderRepository.CreateOrder(order);

            //define mensagens para o cliente
            ViewBag.CheckoutCompleteMessage = "Obrigado pelo seu pedido :)";
            ViewBag.OrderTotal = _shoppingCartRepository.GetShoppingCartTotal();

            //limpar carrinho
            _shoppingCartRepository.CleanCart();

            //exibir view
            return View("~/Views/Order/CheckoutComplete.cshtml", order);
        }

        return View(order);
    }

}