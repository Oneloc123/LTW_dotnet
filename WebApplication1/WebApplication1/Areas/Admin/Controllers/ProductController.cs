using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.UserEdit;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // ======================
        // LIST (Danh sách)
        // ======================
        public IActionResult Index()
        {
            // Lấy danh sách sản phẩm, kèm theo ảnh và thông số để hiển thị nếu cần
            var products = _context.Products
                .Include(p => p.Images)          // Kèm bảng Imagepro
                .Include(p => p.Specifications)  // Kèm bảng Specation
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return View(products);
        }

        // ======================
        // CREATE (Tạo mới)
        // ======================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Products model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Gán ngày tạo mặc định
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;

            // Lưu sản phẩm (Nếu bên View có gửi kèm list Images/Specs thì nó tự lưu luôn)
            _context.Products.Add(model);
            _context.SaveChanges();

            return Redirect("/Admin/Product");
        }

        // ======================
        // EDIT (Sửa - 1 trang bao gồm cả Product, Image, Spec)
        // ======================
        public IActionResult Edit(int id)
        {
            // Tìm sản phẩm và load luôn cả Ảnh + Thông số
            var product = _context.Products
                .Include(p => p.Images)           // Load Imagepro
                .Include(p => p.Specifications)   // Load Specation
                .FirstOrDefault(p => p.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Products model)
        {
            // Tìm sản phẩm cũ trong database
            var product = _context.Products.Find(model.Id);
            if (product == null) return NotFound();

            // Cập nhật thông tin chính (ProductAD)
            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Brand = model.Brand;

            // Xử lý ảnh chính (Nếu có thay đổi link ảnh đại diện)
            if (!string.IsNullOrEmpty(model.MainImageUrl))
            {
                product.MainImageUrl = model.MainImageUrl;
            }

            product.UpdatedAt = DateTime.Now;

            // Lưu lại vào database
            _context.SaveChanges();

            // Lưu ý: Việc sửa chi tiết từng dòng trong List Specifications/Images 
            // thường cần logic phức tạp hơn. Ở mức cơ bản này, ta lưu thông tin chung trước.

            return Redirect("/Admin/Product");
        }

        // ======================
        // DELETE (Xác nhận xoá)
        // ======================
        public IActionResult Delete(int id)
        {
            var product = _context.Products
                .Include(p => p.Images)
                .Include(p => p.Specifications)
                .FirstOrDefault(p => p.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // ======================
        // DELETE (Thực hiện xoá)
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            // Khi xoá Product, EF Core thường sẽ tự xoá Images và Specs đi kèm 
            // (nếu đã cài đặt OnDelete Cascade trong SQL/DbContext)
            _context.Products.Remove(product);
            _context.SaveChanges();

            return Redirect("/Admin/Product");
        }
    }
}