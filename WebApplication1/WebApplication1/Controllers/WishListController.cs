using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.UserEdit;

namespace WebApplication1.Controllers
{
    public class WishListController : Controller
    {
        private readonly AppDbContext _context;

        public WishListController(AppDbContext context)
        {
            _context = context;
        }

        // ---------------------------
        // GET: /WishList
        // ---------------------------
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var wishlist = await _context.WishListItems
                .Where(w => w.UserId == userId)
                .Select(w => new WishListViewModel
                {
                    WishListId = w.Id,
                    ProductId = w.ProductId,
                    ProductName = w.Product.Name,
                    Price = w.Product.Price,
                    ImageUrl = w.Product.Images
                        .Select(i => i.ImageUrl)
                        .FirstOrDefault(),
                    CreatedAt = w.CreatedAt
                })
                .ToListAsync();

            return View(wishlist);
        }

        // ---------------------------
        // POST: /WishList/Add (AJAX)
        // ---------------------------
        [HttpPost]
        public async Task<IActionResult> Add(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            bool exists = await _context.WishListItems
                .AnyAsync(w => w.UserId == userId && w.ProductId == productId);

            if (!exists)
            {
                _context.WishListItems.Add(new WishListItem
                {
                    UserId = userId.Value,
                    ProductId = productId,
                    CreatedAt = DateTime.Now
                });

                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }

        // ---------------------------
        // POST: /WishList/Delete
        // ---------------------------
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var item = await _context.WishListItems
                .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);

            if (item != null)
            {
                _context.WishListItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // ---------------------------
        // GET: /WishList/Count (AJAX)
        // ---------------------------
        [HttpGet]
        public async Task<IActionResult> Count()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Json(new { total = 0 });
            }

            int total = await _context.WishListItems
                .CountAsync(w => w.UserId == userId);

            return Json(new { total });
        }
    }
}
