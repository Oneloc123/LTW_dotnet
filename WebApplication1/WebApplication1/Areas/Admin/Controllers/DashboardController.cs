using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.UserEdit;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Kiểm tra quyền ngay khi vào trang
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || role != "Admin")
            {
                // Nếu không phải Admin, đá về trang Login của khách
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            ViewBag.TotalUsers = _context.Users.Count();
            ViewBag.TotalProducts = _context.Products.Count();
            ViewBag.TotalOrders = _context.Orders.Count();

            var ordersByMonth = _context.Orders
                .GroupBy(o => o.CreatedAt.Month)
                .Select(g => new {
                    Month = g.Key,
                    Count = g.Count()
                }).ToList();

            ViewBag.ChartLabels = ordersByMonth.Select(x => "Tháng " + x.Month).ToList();
            ViewBag.ChartData = ordersByMonth.Select(x => x.Count).ToList();
            ViewBag.TotalContacts = _context.Contacts.Count();
            ViewBag.TotalDiscount = _context.Discounts.Count();

            return View();
        }

    }
}
