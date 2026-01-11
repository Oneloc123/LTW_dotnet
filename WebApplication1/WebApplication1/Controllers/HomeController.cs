using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Models; 
using WebApplication1.Models;
using WebApplication1.Models.BlogEdit;
using WebApplication1.ViewModels;

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
            // Data demo sản phẩm nổi bật
            var products = new List<Products>()
            {
                new Products(){ Id=1, Name="Laptop Gaming MSI", MainImageUrl="/images/laptop.jpg", Price=25000000, Description="Hiệu năng mạnh mẽ" },
                new Products(){ Id=2, Name="Iphone 15 Pro", MainImageUrl="/images/iphone.jpg", Price=32000000, Description="Camera cực tốt" },
                new Products(){ Id=3, Name="Tai nghe Sony", MainImageUrl="/images/headphone.jpg", Price=3500000, Description="Chống ồn tốt" }
            };

            // Data demo blog
            var blogs = new List<Blog>() { };
            /*
            {
                new Blogs(){ Id=1, Title="Tin công nghệ hot nhất 2025", Thumbnail="https://placehold.co/600x400", Summary="Cập nhật xu hướng mới..." },
                new Blogs(){ Id=2, Title="Review laptop gaming", Thumbnail="https://placehold.co/600x400", Summary="Top sản phẩm đáng mua..." }
            };

            ViewBag.Products = featuredProducts;
            */
            ViewBag.Blogs = blogs;
            ViewBag.Products = products;
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
        //public ActionResult ProductDetail(int id)
        //{
        //    // Demo chi tiết sản phẩm
        //    var product = new Products()
        //    {
        //        Id = id,
        //        Name = "Laptop Gaming MSI",
        //        MainImageUrl = "/images/laptop.jpg",
        //        Price = 25000000,
        //        Description = "CPU Core i7, RAM 16GB, SSD 1TB"
        //    };

            return View(product);
        }

        //    return View(product);
        //}
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