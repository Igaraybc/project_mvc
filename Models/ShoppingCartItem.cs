using System.ComponentModel.DataAnnotations;

public class ShoppingCartItem
{
    public int ShoppingCartItemId { get; set; }
    /*Mapeia chave estrangeira para a tabela Snacks: */
    public Snack Snack { get; set; } 
    public int Quantity { get; set; }

    [StringLength(200)]
    public string ShoppingCartId { get; set; } = string.Empty;

}