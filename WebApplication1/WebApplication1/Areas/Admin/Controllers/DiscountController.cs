using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.UserEdit;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiscountController : Controller
    {
        private readonly AppDbContext _db;

        public DiscountController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            var now = DateTime.Now;

            var discounts = _db.Discounts
                .Where(d => d.StartDate <= now && d.EndDate >= now)
                .OrderByDescending(d => d.StartDate)
                .ToList();

            return View(discounts);
        }

        [HttpPost]
        public IActionResult ToggleActive(int id)
        {
            var discount = _db.Discounts.FirstOrDefault(d => d.Id == id);
            if (discount == null) return NotFound();

            discount.IsActive = !discount.IsActive;
            _db.SaveChanges();

            return Redirect("/Admin/Discount");
        }
    }
}
