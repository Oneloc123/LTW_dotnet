using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Models.User.User;
using WebApplication1.Models.UserEdit;


public class ProfileController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;



    public ProfileController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public IActionResult Index()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return NotFound();

        var address = _context.UserAddresses
            .FirstOrDefault(a => a.UserId == userId);

        var blogs = _context.Blogs
    .Where(b => b.UserId == user.Id)
    .ToList();


        var vm = new ProfileEditViewModel
        {
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            Username = user.Username,

            AddressId = address?.Id,
            FullAddress = address?.FullAddress,

            CurrentAvatarUrl = user.AvatarUrl ?? "/uploads/avtars/default-avatar.png",

            Blogs = blogs

        };

        return View(vm);
    }


    [HttpGet]
    [HttpGet]
    public IActionResult Edit()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return NotFound();

        var address = _context.UserAddresses
            .FirstOrDefault(a => a.UserId == userId);

        var vm = new ProfileEditViewModel
        {
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            Username = user.Username,

            AddressId = address?.Id,
            FullAddress = address?.FullAddress,

            CurrentAvatarUrl = user.AvatarUrl ?? "/uploads/avtars/default-avatar.png"

        };

        return View(vm);
    }


    // =======================
    // EDIT PROFILE - POST
    // =======================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProfileEditViewModel model)
    {
        Console.WriteLine(model.AvatarFile == null
    ? "AVATAR FILE = NULL"
    : "AVATAR FILE = OK");

        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return NotFound();

        // ===== Update USER =====
        user.FullName = model.FullName;
        user.PhoneNumber = model.PhoneNumber;
        user.DateOfBirth = model.DateOfBirth;

        // ===== Upload avatar =====
        if (model.AvatarFile != null && model.AvatarFile.Length > 0)
        {
            // 1. Validate file
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/jpg" };
            if (!allowedTypes.Contains(model.AvatarFile.ContentType))
            {
                ModelState.AddModelError("", "Chỉ được upload ảnh JPG, PNG");
                return View(model);
            }

            // 2. Tạo thư mục nếu chưa có
            var uploadPath = Path.Combine(
                _env.WebRootPath,
                "uploads",
                "avatars"
            );


            //Console.WriteLine("UPLOAD PATH = " + uploadPath);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // 3. Tạo tên file
            var fileName = $"user_{user.Id}_{DateTime.Now.Ticks}{Path.GetExtension(model.AvatarFile.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            // 4. Lưu file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.AvatarFile.CopyToAsync(stream);
            }

            // 5. Lưu path vào DB
            user.AvatarUrl = "/uploads/avatars/" + fileName;
        }

        // ===== Update / Insert ADDRESS =====
        var address = _context.UserAddresses
            .FirstOrDefault(a => a.UserId == userId);

        if (address == null)
        {
            // Chưa có → tạo mới
            address = new UserAddress
            {
                UserId = user.Id,
                FullAddress = model.FullAddress
            };
            _context.UserAddresses.Add(address);
        }
        else
        {
            // Có rồi → update
            address.FullAddress = model.FullAddress;
        }

        _context.SaveChanges();
        return RedirectToAction("Index");
    }

}
