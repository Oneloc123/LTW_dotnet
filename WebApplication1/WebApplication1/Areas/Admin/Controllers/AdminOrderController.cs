
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebApplication1.Areas.Admin.Models.Admin.Order;
using WebApplication1.Models.UserEdit;
namespace WebApplication1.Areas.Admin.Controllers
{
    
        [Area("Admin")]
        public class AdminOrderController : Controller
        {
            private readonly AppDbContext _context;

            public AdminOrderController(AppDbContext context)
            {
                _context = context;
            }
        public async Task<IActionResult> Index(
    int? orderId,
    string? customer,
    string? status,
    DateTime? fromDate,
    DateTime? toDate,
    int page = 1,
    int pageSize = 10
)
        {
            var order = _context.Orders
   .Include(o => o.OrderItems)
   .OrderByDescending(o => o.CreatedAt)
   .ToList();
            var query =
                from o in _context.Orders
                join u in _context.Users on o.UserId equals u.Id
                join a in _context.UserAddresses on o.AddressId equals a.Id into addr
                from a in addr.DefaultIfEmpty()
                select new { o, u, a };

            // 🔍 Tìm theo mã đơn
            if (orderId.HasValue)
                query = query.Where(x => x.o.Id == orderId.Value);

            // 🔍 Tìm theo khách hàng
            if (!string.IsNullOrEmpty(customer))
                query = query.Where(x =>
                    x.u.FullName.Contains(customer) ||
                    x.u.Email.Contains(customer)
                );

            // 🔍 Trạng thái
            if (!string.IsNullOrEmpty(status))
                query = query.Where(x => x.o.Status == status);

            // 🔍 Thời gian
            if (fromDate.HasValue)
                query = query.Where(x => x.o.CreatedAt >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(x => x.o.CreatedAt <= toDate.Value);

            // 📌 Sắp xếp mới nhất
            query = query.OrderByDescending(x => x.o.CreatedAt);

            // 📌 Tổng số bản ghi
            int totalItems = await query.CountAsync();

            // 📌 Phân trang + map ViewModel
            var orders = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new AdminOrderViewModel
                {
                    OrderId = x.o.Id,
                    CustomerName = x.u.FullName,
                    Phone = x.u.PhoneNumber,
                    Email = x.u.Email,
                    ShippingAddress = x.a != null ? x.a.FullAddress : "",
                    TotalPrice = x.o.TotalPrice,
                    Status = x.o.Status,
                    CreatedAt = x.o.CreatedAt,
                    FirstProductName = x.o.GetFirstProductName(),
                    FirstImageUrl = x.o.GetFirstImageUrl(),
                    ItemCount = x.o.OrderItems.Count
                })
                .ToListAsync();

            ViewBag.TotalItems = totalItems;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data =
                await (
                    from o in _context.Orders
                    join u in _context.Users on o.UserId equals u.Id
                    join a in _context.UserAddresses on o.AddressId equals a.Id into addr
                    from a in addr.DefaultIfEmpty()
                    where o.Id == id
                    select new AdminOrderDetailViewModel
                    {
                        OrderId = o.Id,
                        CreatedAt = o.CreatedAt,
                        Status = o.Status,
                        TotalPrice = o.TotalPrice,

                        CustomerName = u.FullName,
                        Phone = u.PhoneNumber,
                        Email = u.Email,

                        ShippingAddress = a != null ? a.FullAddress : "",

                        OrderItems = o.OrderItems
                    }
                ).FirstOrDefaultAsync();

            if (data == null)
                return NotFound();

            return View(data);
        }



    }


}
