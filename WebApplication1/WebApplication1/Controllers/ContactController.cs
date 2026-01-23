using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.UserEdit;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;

        public ContactController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Contact model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                model.CreatedAt = DateTime.Now;
                model.IsRead = false;

                _db.Contacts.Add(model);
                _db.SaveChanges();

                TempData["success"] = "Gửi liên hệ thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Có lỗi xảy ra, vui lòng thử lại sau.");
                return View(model);
            }
        }

    }
}
