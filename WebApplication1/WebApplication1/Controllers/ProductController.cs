//using Microsoft.AspNetCore.Mvc;
//using WebApplication1.Models;
//using Microsoft.EntityFrameworkCore;

//namespace WebApplication1.Controllers
//{
//    public class ProductController : Controller
//    {
//        private readonly WebApplication1.Models.AppDbContext _context;

//        public ProductController(WebApplication1.Models.AppDbContext context)
//        {
//            _context = context;
//        }

//        string connectionString = "server=localhost;port=3306;database=netdb;user=root;password=";


//        //Index
//        public async Task<IActionResult> Index(string brand, decimal? minPrice, decimal? maxPrice, string category, string searchString)
//        {

//            var products = _context.Products.AsQueryable();


//            if (!string.IsNullOrEmpty(brand))
//            {
//                products = products.Where(p => p.Brand == brand);
//            }


//            if (minPrice.HasValue)
//            {
//                products = products.Where(p => p.Price >= minPrice.Value);
//            }


//            if (maxPrice.HasValue)
//            {
//                products = products.Where(p => p.Price <= maxPrice.Value);
//            }



//            if (!string.IsNullOrEmpty(searchString))
//            {
//                products = products.Where(p => p.Name.Contains(searchString));
//            }


//            ViewBag.CurrentBrand = brand;
//            ViewBag.CurrentMinPrice = minPrice;
//            ViewBag.CurrentMaxPrice = maxPrice;


//            return View(await products.ToListAsync());
//        }


//        //Details
//        public IActionResult Detail(int id)
//        {

//            var product = _context.Products.Include(p => p.Reviews).FirstOrDefault(p => p.Id == id);

//            if (product == null)
//                return NotFound();

//            return View(product);
//        }

//        //Reviews
//        [HttpPost]
//        public IActionResult AddReview(int productId, string comment, int rating)
//        {
//            // Mặc định tên là "Guest"
//            string tenNguoiDung = "Guest";

//            // Nếu hệ thống phát hiện đã đăng nhập, thì lấy tên User đè vào
//            if (User.Identity.IsAuthenticated)
//            {
//                tenNguoiDung = User.Identity.Name ?? "User";
//            }

//            // Tạo Review
//            var newReview = new WebApplication1.Models.Reviews
//            {
//                ProductId = productId,
//                Comment = comment,
//                Rating = rating,
//                UserName = tenNguoiDung, // Gán cái tên đã xử lý ở trên vào đây
//                CreatedAt = DateTime.Now
//            };

//            // Lưu vào Database
//            _context.Reviews.Add(newReview);
//            _context.SaveChanges();

//            return RedirectToAction("Detail", new { id = productId });
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {

        private readonly AppDbContext db;

        public ProductController(AppDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var products = db.Products.ToList();
            return View(products);
        }

        public IActionResult Detail(int id)
        {
            var product = db.Products
                .Include(p => p.ProductVariants)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}

