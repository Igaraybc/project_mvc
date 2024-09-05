public class OrderSnackViewModel
{
    public Order Order { get; set; }
    public IEnumerable<DetailOrder> DetailOrders { get; set; }
}