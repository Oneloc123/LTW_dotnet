using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Helpers;
namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET: /Account/Login
        // =========================
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // =========================
        // POST: /Account/Login
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            // Hash mật khẩu người dùng nhập
            string passwordHash = PasswordHelper.Hash(model.Password);

            // Tìm user theo Username hoặc Email
            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    (u.Username == model.UsernameOrEmail ||
                     u.Email == model.UsernameOrEmail));

            // Không tồn tại user
            if (user == null)
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                return View(model);
            }

            // Tài khoản bị khóa vĩnh viễn
            if (!user.IsActive)
            {
                ModelState.AddModelError("", "Tài khoản đã bị vô hiệu hóa");
                return View(model);
            }

            // Tài khoản đang bị khóa tạm thời
            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.Now)
            {
                ModelState.AddModelError("",
                    $"Tài khoản bị khóa đến {user.LockoutEnd:dd/MM/yyyy HH:mm}");
                return View(model);
            }

            // Sai mật khẩu
            if (user.PasswordHash != passwordHash)
            {
                user.FailedLoginCount++;

                // Sai 5 lần thì khóa 15 phút
                if (user.FailedLoginCount >= 5)
                {
                    user.LockoutEnd = DateTime.Now.AddMinutes(15);
                    user.FailedLoginCount = 0;
                }

                await _context.SaveChangesAsync();

                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                return View(model);
            }

            // =========================
            // LOGIN THÀNH CÔNG
            // =========================

            user.FailedLoginCount = 0;
            user.LockoutEnd = null;
            user.LastLoginAt = DateTime.Now;

            await _context.SaveChangesAsync();

            // Lưu Session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            // Remember me (basic)
            if (model.RememberMe)
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7),
                    HttpOnly = true
                };
                Response.Cookies.Append("Username", user.Username, options);
            }

            return RedirectToAction("Index", "Home");
        }

        // =========================
        // LOGOUT
        // =========================
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("Username");
            return RedirectToAction("Login");
        }
        // =========================
        // GET: /Account/Register
        // =========================
        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // =========================
        // POST: /Account/Register
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Kiểm tra Username đã tồn tại
            bool usernameExists = await _context.Users
                .AnyAsync(u => u.Username == model.Username);
            if (usernameExists)
            {
                ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại");
                return View(model);
            }

            // Kiểm tra Email đã tồn tại
            bool emailExists = await _context.Users
                .AnyAsync(u => u.Email == model.Email);
            if (emailExists)
            {
                ModelState.AddModelError("Email", "Email đã được sử dụng");
                return View(model);
            }

            // Hash mật khẩu
            string passwordHash = PasswordHelper.Hash(model.Password);

            // Tạo user mới
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = passwordHash,

                Role = "User",
                IsActive = true,
                EmailConfirmed = false,

                CreatedAt = DateTime.Now,
                AvatarUrl = "/images/default-avatar.png",
                FullName = "",
                PhoneNumber = "",
                Gender = 0
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // (Tuỳ chọn) Tự động đăng nhập sau khi đăng ký
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Index", "Home");
        }

    }
}

