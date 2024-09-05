public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    private readonly ShoppingCart _shoppingCart;

    public OrderRepository(AppDbContext context, ShoppingCart shoppingCart)
    {
        _context = context;
        _shoppingCart = shoppingCart;
    }

    public void CreateOrder(Order order)
    {
        order.OrderSent = DateTime.Now;
        _context.Orders.Add(order);
        _context.SaveChanges();

        var shoppingCart = _shoppingCart.ShoppingCartItems;

        shoppingCart.ForEach(item => {
            var detailOrder = new DetailOrder{
                Quantity = item.Quantity,
                SnackId = item.Snack.SnackId,
                OrderId = order.OrderId,
                Price = item.Snack.Price
            };
            _context.DetailOrders.Add(detailOrder);
        });
        _context.SaveChanges();

    }
}