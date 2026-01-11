using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Models; 

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        
        private readonly AppDbContext _context;

        
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            
            var featuredProducts = _context.Products
                .Include(p => p.Reviews)
                .OrderByDescending(p => p.Reviews.Sum(r => r.Rating))
                .Take(6)
                .ToList();

            
            var blogs = new List<Blogs>()
            {
                new Blogs(){ Id=1, Title="Tin công nghệ hot nhất 2025", Thumbnail="https://placehold.co/600x400", Summary="Cập nhật xu hướng mới..." },
                new Blogs(){ Id=2, Title="Review laptop gaming", Thumbnail="https://placehold.co/600x400", Summary="Top sản phẩm đáng mua..." }
            };

            ViewBag.Products = featuredProducts;
            ViewBag.Blogs = blogs;

            return View();
        }

        public IActionResult ProductDetail(int id)
        {
            
            var product = _context.Products
                .Include(p => p.Reviews)
                .Include(p => p.Specifications)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}