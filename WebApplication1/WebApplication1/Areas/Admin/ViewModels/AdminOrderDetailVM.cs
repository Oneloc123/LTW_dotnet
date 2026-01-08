using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.ViewModels
{
    public class AdminOrderDetailVM
    {
        public Orders Order { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
