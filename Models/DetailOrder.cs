using System.ComponentModel.DataAnnotations.Schema;

public class DetailOrder
{
    public int DetailOrderId { get; set; }
    public int OrderId { get; set; }
    public int SnackId { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public virtual Snack Snack{ get; set; }
    public virtual Order Order{ get; set; }

}