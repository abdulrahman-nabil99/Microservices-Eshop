namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = string.Empty;
        public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
        public ShoppingCart() { }
        public ShoppingCart(string userName) 
        {
            UserName = userName;
        }

    }
}
