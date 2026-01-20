using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Models; 

using WebApplication1.Models.BlogEdit;
using WebApplication1.Models.UserEdit;
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

            var featuredProducts = _context.Products;
                //.Include(p => p.Images)           
                //.Include(p => p.Reviews)          
                //.OrderByDescending(p => p.Reviews.Average(r => r.Rating)) 
                //.Take(6)                          
                //.ToList();

            
            var blogs = _context.Blogs
                .OrderByDescending(b => b.ViewCount) 
                .Take(2)
                .ToList();

           
            ViewBag.Products = featuredProducts;
            ViewBag.Blogs = blogs.ToList();

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