using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models.UserEdit;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // ======================
        // LIST
        // ======================
        public IActionResult Index()
        {
            var users = _context.Users
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return View(users);
        }

        // ======================
        // CREATE
        // ======================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            user.PasswordHash = PasswordHelper.Hash(user.PasswordHash);
            user.CreatedAt = DateTime.Now;

            _context.Users.Add(user);
            _context.SaveChanges();

            return Redirect("/Admin/User");
        }

        // ======================
        // EDIT
        // ======================
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.Find(model.Id);
            if (user == null) return NotFound();

            user.Email = model.Email;
            user.FullName = model.FullName;
            user.PhoneNumber = model.PhoneNumber;
            user.Role = model.Role;
            user.IsActive = model.IsActive;
            user.Gender = model.Gender;
            user.DateOfBirth = model.DateOfBirth;
            user.UpdatedAt = DateTime.Now;

            _context.SaveChanges();

            return Redirect("/Admin/User");
        }

        // ======================
        // DELETE (CONFIRM)
        // ======================
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // ======================
        // DELETE (POST)
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Redirect("/Admin/User");
        }
    }
}
