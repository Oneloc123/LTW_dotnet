using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.UserEdit;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;

        public ContactController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var contacts = _db.Contacts
                                   .OrderByDescending(c => c.CreatedAt)
                                   .ToList();

            return View(contacts);
        }

        [HttpPost]
        public IActionResult MarkAsRead(int id)
        {
            var contact = _db.Contacts.FirstOrDefault(c => c.Id == id);

            if (contact == null)
                return NotFound();

            // Đã đọc rồi thì không làm gì nữa
            if (contact.IsRead)
                return Redirect("/Admin/Contact");

            contact.IsRead = true;
            _db.SaveChanges();

            return Redirect("/Admin/Contact");
        }

        public IActionResult Details(int id)
        {
            var contact = _db.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null) return NotFound();

            // vào xem là coi như đã đọc
            if (!contact.IsRead)
            {
                contact.IsRead = true;
                _db.SaveChanges();
            }

            return View(contact);
        }

    }
}
