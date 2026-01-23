using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.BlogEdit;
using WebApplication1.Models.UserEdit;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("Admin/[controller]/[action]")]
    public class AdminBlogController : Controller
    {
        private readonly AppDbContext _context;

        public AdminBlogController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET: /Admin/AdminBlog
        // =========================
        public IActionResult Index()
        {
            var blogs = _context.Blogs
                .OrderByDescending(b => b.CreatedAt)
                .ToList();

            return View(blogs);
        }

        // =========================
        // GET: /Admin/AdminBlog/Details/5
        // =========================
        public IActionResult Details(int id)
        {
            var blog = _context.Blogs
                .Include(b => b.User)
                .Include(b => b.Comments)
                    .ThenInclude(c => c.User)
                .Include(b => b.Ratings)
                .FirstOrDefault(b => b.Id == id);

            if (blog == null)
                return NotFound();

            return View(blog);
        }

        // =========================
        // POST: /Admin/AdminBlog/Delete/5
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var blog = _context.Blogs.Find(id);

            if (blog == null)
                return NotFound();

            _context.Blogs.Remove(blog);
            _context.SaveChanges();

            TempData["success"] = "Đã xóa bài viết";

            // QUAN TRỌNG: redirect đúng controller hiện tại
            return RedirectToAction(nameof(Index));
        }
    }
}

