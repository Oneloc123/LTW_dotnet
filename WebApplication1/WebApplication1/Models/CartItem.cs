namespace WebApplication1.Models
{
    public class CartItem
    {
        public int VariantId { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }

        public string Color { get; set; }
        public string Memory { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public decimal FinalPrice => Price - Price * Discount / 100m;
        public decimal Total => FinalPrice * Quantity;

    }
}
