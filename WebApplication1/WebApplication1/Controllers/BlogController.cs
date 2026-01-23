using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenXmlPowerTools;
using System.Xml.Linq;
using WebApplication1.Models.BlogEdit;
using WebApplication1.Models.UserEdit;


namespace WebApplication1.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search, DateTime? fromDate)
        {
            // Khi cần duyệt mới bật
            //var blogs = _context.Blogs
            //    .Where(b => b.IsApproved)
            //    .AsQueryable();
            var blogs = _context.Blogs.AsQueryable();


            if (!string.IsNullOrEmpty(search))
                blogs = blogs.Where(b => b.Title.Contains(search));

            if (fromDate.HasValue)
                blogs = blogs.Where(b => b.CreatedAt >= fromDate);

            ViewBag.FeaturedBlogs = blogs
                .OrderByDescending(b => b.ViewCount)
                .Take(5)
                .ToList();

            return View(blogs.OrderByDescending(b => b.CreatedAt).ToList());
        }
        public IActionResult Details(int id)
        {

            var blog = _context.Blogs
                .Include(b => b.User)
                .Include(b => b.Comments)
                    .ThenInclude(c => c.User)
                .Include(b => b.Ratings)
                .FirstOrDefault(b => b.Id == id);

            if (blog == null) return NotFound();

            blog.ViewCount++;
            _context.SaveChanges();

            ViewBag.AverageRating = blog.Ratings.Any()
                ? blog.Ratings.Average(r => r.Rating)
                : 0;

            return View(blog);
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title,string? img, string content, IFormFile wordFile)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");
            int? userId = HttpContext.Session.GetInt32("UserId");
            
           

            
            if (wordFile != null && wordFile.Length > 0)
            {
                using var stream = wordFile.OpenReadStream();
                content = WordHelper.ReadWordAsHtml(stream);
            }

            
            var blog = new Blog
            {
                Title = title,
                Content = content,
                UserId = GetCurrentUserId(),
                Thumbnail = img,
                CreatedAt = DateTime.Now
            };

            _context.Blogs.Add(blog);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
       
        [HttpPost]
        public IActionResult AddComment(int blogId, string content)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var comment = new BlogComment
            {
                BlogId = blogId,
                UserId = GetCurrentUserId(),
                Content = content,
                CreatedAt = DateTime.Now
            };

            _context.BlogComments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = blogId });
        }
        [HttpPost]
        public IActionResult Rate(int blogId, int rating)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");
            var exist = _context.BlogRatings
                .FirstOrDefault(r => r.BlogId == blogId && r.UserId == GetCurrentUserId());

            if (exist == null)
            {
                _context.BlogRatings.Add(new BlogRating
                {
                    BlogId = blogId,
                    UserId = GetCurrentUserId(),
                    Rating = rating
                });
                _context.SaveChanges();
            }

            return RedirectToAction("Details", new { id = blogId });
        }
        public int GetCurrentUserId()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                throw new UnauthorizedAccessException("User chưa đăng nhập");

            return userId.Value;
        }
        [HttpPost]
        public IActionResult DeleteComment(int id)
        {
            if (!IsLoggedIn())
                return Unauthorized();

            var comment = _context.BlogComments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
                return NotFound();

            // ❌ Không cho xóa comment của người khác
            if (comment.UserId != GetCurrentUserId())
                return Forbid();

            _context.BlogComments.Remove(comment);
            _context.SaveChanges();

            return Ok();
        }

        



        public static class WordHelper
        {
            public static string ReadWordAsHtml(Stream inputStream)
            {
                // ⭐ COPY sang MemoryStream (bắt buộc)
                using var ms = new MemoryStream();
                inputStream.CopyTo(ms);
                ms.Position = 0;

                using var wordDoc = WordprocessingDocument.Open(ms, true); // ⭐ true = write

                var settings = new HtmlConverterSettings
                {
                    PageTitle = "Blog",
                    FabricateCssClasses = true
                };

                XElement html = HtmlConverter.ConvertToHtml(wordDoc, settings);
                return html.ToString(SaveOptions.DisableFormatting);
            }
        }
        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetInt32("UserId") != null;
        }


    }

}
