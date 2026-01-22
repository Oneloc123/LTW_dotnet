using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models.UserEdit;
using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WishListController : Controller
    {
        private readonly AppDbContext _context;

        public WishListController(AppDbContext context)
        {
            _context = context;
        }

        // LIST
        public IActionResult Index()
        {
            var data = _context.WishListItems
                .Include(w => w.UserId)
                .Include(w => w.Product)
                .ToList();

            return View(data);
        }

        // CREATE
        public IActionResult Create()
        {
            ViewBag.Users = new SelectList(_context.Users, "Id", "Email");
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(WishListItem item)
        {
            if (ModelState.IsValid)
            {
                item.CreatedAt = DateTime.Now;
                _context.WishListItems.Add(item);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var item = _context.WishListItems
                .Include(w => w.User)
                .Include(w => w.Product)
                .FirstOrDefault(w => w.Id == id);

            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = _context.WishListItems.Find(id);
            _context.WishListItems.Remove(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
