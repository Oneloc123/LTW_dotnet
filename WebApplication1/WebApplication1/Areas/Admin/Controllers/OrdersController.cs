//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebApplication1.Models;
//using WebApplication1.Areas.Admin.ViewModels;

//namespace WebApplication1.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class OrdersController : Controller
//    {
//        private readonly AppDbContext _context;
//        public OrdersController(AppDbContext context) => _context = context;

//        // Danh sách đơn
//        public IActionResult Index()
//        {
//            var orders = _context.Orders
//                .OrderByDescending(o => o.CreatedAt)
//                .ToList();
//            return View(orders);
//        }

//        // Chi tiết đơn
//        public IActionResult Details(int id)
//        {
//            var order = _context.Orders
//                .Include(o => o.OrderItems)
//                    .ThenInclude(i => i.Product)
//                .FirstOrDefault(o => o.Id == id);

//            if (order == null) return NotFound();

//            var vm = new AdminOrderDetailVM
//            {
//                Order = order,
//                Items = order.OrderItems.ToList()
//            };

//            return View(vm);
//        }

//        // Cập nhật trạng thái
//        [HttpPost]
//        public IActionResult UpdateStatus(int id, string status)
//        {
//            var order = _context.Orders.Find(id);
//            if (order == null) return NotFound();

//            order.Status = status;
//            _context.SaveChanges();

//            return RedirectToAction("Details", new { id });
//        }
//    }
//}
