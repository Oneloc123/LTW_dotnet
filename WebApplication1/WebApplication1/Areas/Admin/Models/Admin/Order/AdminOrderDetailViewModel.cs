using WebApplication1.Models.OrderEdit.Order;

namespace WebApplication1.Areas.Admin.Models.Admin.Order
{
    public class AdminOrderDetailViewModel
    {
        // Order
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }

        // Khách hàng
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // Địa chỉ
        public string ShippingAddress { get; set; }

        // Sản phẩm
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
