using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            // test giao diện
            var items = new List<CartItem>
        {
                new CartItem { Id = 1, Name = "Xiaomi 15T Pro 5G 12GB 512GB Xám", Price = 19490000, Quantity = 1, Discount = 0, ImageUrl = "/images/xiaomi_15t_pro_xam_5_a006830687.webp" },
                new CartItem { Id = 2, Name = "iPhone 16 Pro 128GB Titan Tự Nhiên MYNG3VN/A", Price = 25590000, Quantity = 2, Discount = 15, ImageUrl = "/images/iphone_16_pro_natural_titan_412b47e840.webp" }
        };

            return View(items);
        }

        [HttpPost]
        public IActionResult Pay(string fullname, string phone, string address)
        {
            return Content("Thanh toán thành công");
        }
    }
}
