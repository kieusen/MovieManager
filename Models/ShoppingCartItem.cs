namespace Movie_01.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }        
    }
}