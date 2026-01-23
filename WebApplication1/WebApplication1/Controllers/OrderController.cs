using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.UserEdit;

namespace WebApplication1.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Order
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var orders = _context.Orders
    .Where(o => o.UserId == userId)
    .Include(o => o.OrderItems)   
    .OrderByDescending(o => o.CreatedAt)
    .ToList();


            return View(orders);
        }

        // Xem chi tiết đơn hàng
        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound();

            // Load Variant thủ công
            var variantIds = order.OrderItems
                .Select(i => i.ProductId) // thực chất là VariantId
                .ToList();

            var variants = _context.ProductVariants
                .Include(v => v.Product)
                .Where(v => variantIds.Contains(v.Id))
                .ToList();

            ViewBag.Variants = variants;

            return View(order);
        }


    }

}
