using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.OrderEdit.Order;
using WebApplication1.Models.UserEdit;

namespace WebApplication1.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _context;
        public CheckoutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = CartSessionHelper.GetCart(HttpContext);

            if (!cart.Items.Any())
                return RedirectToAction("Index", "Cart");

            return View(cart);
        }

        [HttpPost]
        public IActionResult Pay(string fullname, string phone, string address)
        {


            return Content("Thanh toán thành công");
        }

        public IActionResult PlaceOrder()
        {
            // Lấy giỏ hàng từ session
            var cart = CartSessionHelper.GetCart(HttpContext);

            // Kiểm tra giỏ hàng có rỗng không
            if (cart == null || cart.TotalQuantity == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            // Lấy thông tin người dùng từ session
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            // Tạo đơn hàng mới
            var order = new Order
            {
                UserId = userId.Value,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            decimal total = 0;

            // Fix: Use cart.ItemsDict to enumerate cart items
            foreach (var entry in cart.ItemsDict)
            {
                int productId = entry.Key;
                CartItem cartItem = entry.Value;

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,                 // FK
                    ProductId = productId,              // từ key
                    Quantity = cartItem.Quantity
                };

                total += cartItem.Quantity * cartItem.Price;

                _context.OrderItems.Add(orderItem);
            }

            return Content("Order placed successfully");
        }
    }
}
