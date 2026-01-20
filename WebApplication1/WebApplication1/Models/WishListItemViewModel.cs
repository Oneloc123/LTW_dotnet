namespace WebApplication1.Models
{
    public class WishListViewModel
    {
        public int WishListId { get; set; }
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
