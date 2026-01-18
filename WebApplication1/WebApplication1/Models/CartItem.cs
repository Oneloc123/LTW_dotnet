using WebApplication1.Models.Enum;

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

        // Sửa discount
        // ===== DISCOUNT =====
        public CartDiscountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }

        // Giá cuối 1 sản phẩm 
        // ===== TÍNH GIÁ =====
        public decimal FinalPrice
        {
            get
            {
                decimal price = Price;

                switch (DiscountType)
                {
                    case CartDiscountType.Percentage:
                        price -= price * DiscountValue / 100m;
                        break;

                    case CartDiscountType.Amount:
                        price -= DiscountValue;
                        break;

                    case CartDiscountType.None:
                    default:
                        break;
                }

                return Math.Max(0, price);
            }
        }

        public decimal Total => FinalPrice * Quantity;
 
        public List<(string Color, string ImageUrl)> AvailableColors { get; set; } = new();
    }
}