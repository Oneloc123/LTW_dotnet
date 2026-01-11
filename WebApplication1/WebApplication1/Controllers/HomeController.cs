using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Models.BlogEdit;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
                    
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Data demo sản phẩm nổi bật
            var products = new List<Products>()
            {
                new Products(){ Id=1, Name="Laptop Gaming MSI", Image="/images/laptop.jpg", Price=25000000, Description="Hiệu năng mạnh mẽ" },
                new Products(){ Id=2, Name="Iphone 15 Pro", Image="/images/iphone.jpg", Price=32000000, Description="Camera cực tốt" },
                new Products(){ Id=3, Name="Tai nghe Sony", Image="/images/headphone.jpg", Price=3500000, Description="Chống ồn tốt" }
            };

            // Data demo blog
            var blogs = new List<Blog>() { };
            /*
            {
                new Blogs(){ Id=1, Title="Tin công nghệ hot nhất 2025", Thumbnail="/images/blog1.jpg", Summary="Cập nhật xu hướng mới..." },
                new Blogs(){ Id=2, Title="Review laptop gaming", Thumbnail="/images/blog2.jpg", Summary="Top sản phẩm đáng mua..." }
            };

            */
            ViewBag.Blogs = blogs;
            ViewBag.Products = products;
            return View();
        }
        public ActionResult ProductDetail(int id)
        {
            // Demo chi tiết sản phẩm
            var product = new Products()
            {
                Id = id,
                Name = "Laptop Gaming MSI",
                Image = "/images/laptop.jpg",
                Price = 25000000,
                Description = "CPU Core i7, RAM 16GB, SSD 1TB"
            };

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
