using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.UserEdit;
using WebApplication1.ViewModels;
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
            HttpContext.Session.SetString("Role", user.Role); // Lưu quyền để check ở View
            if (user.AvatarUrl != null)
            {
                HttpContext.Session.SetString("avatar", user.AvatarUrl);
            }

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

            if (user.Role == "Admin")
            {
                return RedirectToAction("Index", "User", new { area = "Admin" });
            }

            // Nếu không phải Admin, về trang chủ người dùng (không có area)
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // =========================
        // LOGOUT
        // =========================
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("Username");
            return RedirectToAction("Login", "Account", new { area = "" });
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // 1. Kiểm tra Username tồn tại
            bool usernameExists = await _context.Users.AnyAsync(u => u.Username == model.Username);
            if (usernameExists)
            {
                ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại");
                return View(model);
            }

            // 2. Kiểm tra Email tồn tại
            bool emailExists = await _context.Users.AnyAsync(u => u.Email == model.Email);
            if (emailExists)
            {
                ModelState.AddModelError("Email", "Email đã được sử dụng");
                return View(model);
            }

            // 3. Hash mật khẩu
            string passwordHash = PasswordHelper.Hash(model.Password);

            // 4. Tạo user mới (Gán đủ các giá trị để tránh lỗi Null trong Dashboard)
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = passwordHash,

                Role = "User", // Mặc định luôn là User để bảo mật
                IsActive = true,
                EmailConfirmed = false,

                // Quan trọng: Gán giá trị cụ thể thay vì để mặc định từ Model 
                // để chắc chắn dữ liệu đồng bộ vào DB
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,

                AvatarUrl = "/uploads/avatars/default-avatar.png",
                FullName = "",
                PhoneNumber = "",
                Gender = 0,
                FailedLoginCount = 0
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // 5. Tự động đăng nhập sau khi đăng ký
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);
            HttpContext.Session.SetString("avatar", user.AvatarUrl);

            // Vì mới đăng ký mặc định là "User", nên trả về Home chính
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login");

            var user = await _context.Users.FindAsync(userId);

            // Check mật khẩu hiện tại
            if (user.PasswordHash != PasswordHelper.Hash(model.CurrentPassword))
            {
                ModelState.AddModelError("", "Mật khẩu hiện tại không đúng");
                return View(model);
            }

            // Sinh OTP
            string otp = new Random().Next(100000, 999999).ToString();

            // Lưu tạm vào Session (5 phút)
            HttpContext.Session.SetString("ChangePasswordOtp", otp);
            HttpContext.Session.SetString("NewPasswordTemp",
                PasswordHelper.Hash(model.NewPassword));
            HttpContext.Session.SetString("OtpExpire",
                DateTime.Now.AddMinutes(5).ToString());

            // Gửi email
            EmailHelper.Send(
                user.Email,
                "Xác thực đổi mật khẩu",
                $"Mã xác thực của bạn là: {otp}\nCó hiệu lực 5 phút."
            );

            return RedirectToAction("VerifyChangePassword");
        }
        [HttpGet]
        public IActionResult VerifyChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyChangePassword(VerifyChangePasswordViewModel model)
        {
            string otpSession = HttpContext.Session.GetString("ChangePasswordOtp");
            string otpExpire = HttpContext.Session.GetString("OtpExpire");

            if (otpSession == null || DateTime.Parse(otpExpire) < DateTime.Now)
            {
                ModelState.AddModelError("", "Mã xác thực đã hết hạn");
                return View(model);
            }

            if (model.Otp != otpSession)
            {
                ModelState.AddModelError("", "Mã xác thực không đúng");
                return View(model);
            }

            int userId = HttpContext.Session.GetInt32("UserId").Value;
            var user = await _context.Users.FindAsync(userId);

            user.PasswordHash = HttpContext.Session.GetString("NewPasswordTemp");
            await _context.SaveChangesAsync();

            // Xoá session tạm
            HttpContext.Session.Remove("ChangePasswordOtp");
            HttpContext.Session.Remove("NewPasswordTemp");
            HttpContext.Session.Remove("OtpExpire");

            TempData["Success"] = "Đổi mật khẩu thành công!";
            return RedirectToAction("Index", "Profile");
        }
        
    }


}

