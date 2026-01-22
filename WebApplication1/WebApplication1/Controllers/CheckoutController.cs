using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.Checkout;
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
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return RedirectToAction("Login", "Account");


            var addresses = _context.UserAddresses
             .Where(a => a.UserId == userId.Value)
             .ToList();

            var vm = new Checkout
            {
                FullName = user.FullName,
                Phone = user.PhoneNumber,
                Cart = cart,
                Addresses = addresses
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Pay(string fullname, string phone, int addressid)
        {
            var cart = CartSessionHelper.GetCart(HttpContext);

            if (!cart.Items.Any())
                return RedirectToAction("Index", "Cart");

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");
      
            var order = new Order
            {
                UserId = userId.Value,
                AddressId = addressid,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in cart.Items)
            {
                _context.OrderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    VariantId = item.VariantId,
                    Quantity = item.Quantity,
                });
            }

            _context.SaveChanges();

            // XÓA CART SAU KHI ĐẶT
            CartSessionHelper.Clear(HttpContext);

            return RedirectToAction("Details", "Order", new { id = order.Id });
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
                return RedirectToAction("Login", "Account");
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
