namespace WebApplication1.Areas.Admin.Models.Admin.Order
{
    public class AdminOrderViewModel
    {
        public int OrderId { get; set; }

        // Khách hàng
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Địa chỉ
        public string ShippingAddress { get; set; } = string.Empty;

        // Order
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Hiển thị nhanh
        public string FirstProductName { get; set; } = string.Empty;
        public string FirstImageUrl { get; set; } = "/images/no-image.png";
        public int ItemCount { get; set; }
    }
}
