public interface IShoppingCartRepository
{
    public void AddItemToCart(Snack snack);
    public int RemoveFromCart(Snack snack);
    public List<ShoppingCartItem> GetShoppingCartItems();
    public void CleanCart();
    public decimal GetShoppingCartTotal();
}