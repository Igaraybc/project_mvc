public class SalesChartService
{
    private readonly AppDbContext _context;

    public SalesChartService(AppDbContext context)
    {
        _context = context;
    }

     public List<SnackChart> GetSnackSales (int days = 360){
        var date = DateTime.UtcNow.AddDays(-days);

        var snacks = from pd in _context.DetailOrders join s in _context.Snacks on pd.SnackId equals s.SnackId
                        where pd.Order.OrderSent >= date group pd by new { pd.SnackId, s.SnackName}
                        into g select new {
                            SnackName = g.Key.SnackName,
                            SnackQuantity = g.Sum(q => q.Quantity),
                            SnackTotalValue = g.Sum(a => a.Price * a.Quantity)
                        };
        var list = new List<SnackChart>();
        foreach(var item in snacks){
            var snack = new SnackChart();
            snack.SnackName = item.SnackName;
            snack.SnackQuantity = item.SnackQuantity;
            snack.SnackTotalValue = item.SnackTotalValue;
            list.Add(snack);
        }

        return list;
        
     }
}