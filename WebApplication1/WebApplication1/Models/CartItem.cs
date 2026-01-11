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

        // Giảm giá theo %
        public int Discount { get; set; }

        // Giảm theo tiền cố định (vd: 99k)
        public decimal DiscountAmount { get; set; } = 0;

        // Giá cuối 1 sản phẩm
        public decimal FinalPrice => Math.Max(0, Price - Price * Discount / 100m - DiscountAmount);

        public decimal Total => FinalPrice * Quantity;

        public List<(string Color, string ImageUrl)> AvailableColors { get; set; } = new();
    }
}
