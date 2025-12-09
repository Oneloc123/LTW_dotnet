using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        // Tạm thời lưu user trong list (demo)
        //static List<UserRegisterModel> users = new List<UserRegisterModel>();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }
        // List user demo 
        //static readonly System.Collections.Generic.List<UserRegisterModel> users
        //    = new System.Collections.Generic.List<UserRegisterModel>();

        // GET: Change Password
        public ActionResult ChangePassword()
        {
            //// Yêu cầu đăng nhập
            //if (Session["User"] == null)
            //    return RedirectToAction("Login");

            return View();
        }
        //// POST: Login
        //[HttpPost]
        //public ActionResult Login(UserLoginModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // Demo: check cố định
        //    if (model.Username == "admin" && model.Password == "123")
        //    {
        //        // Lưu session
        //        Session["User"] = model.Username;
        //        return RedirectToAction("Index", "Home");
        //    }

        //    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");

        //    return View(model);
        //}

        //public ActionResult Logout()
        //{
        //    Session.Clear();
        //    return RedirectToAction("Login");
        //}

        // POST: Register
        //[HttpPost]
        //public ActionResult Register(UserRegisterModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // Kiểm tra trùng username
        //    foreach (var u in users)
        //    {
        //        if (u.Username == model.Username)
        //        {
        //            ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!");
        //            return View(model);
        //        }
        //    }

        //    // Lưu user mới
        //    users.Add(model);

        //    TempData["Success"] = "Đăng ký thành công! Mời bạn đăng nhập.";

        //    return RedirectToAction("Login", "Account");
        //}

        //// POST: Change Password
        //[HttpPost]
        //public ActionResult ChangePassword(ChangePasswordModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    string username = Session["User"].ToString();

        //    // Tìm user trong list
        //    var user = users.FirstOrDefault(x => x.Username == username);

        //    if (user == null)
        //    {
        //        ModelState.AddModelError("", "Không tìm thấy tài khoản!");
        //        return View(model);
        //    }

        //    // Kiểm tra mật khẩu cũ
        //    if (user.Password != model.OldPassword)
        //    {
        //        ModelState.AddModelError("", "Mật khẩu cũ không chính xác!");
        //        return View(model);
        //    }

        //    // Cập nhật mật khẩu mới
        //    user.Password = model.NewPassword;

        //    TempData["Success"] = "Đổi mật khẩu thành công!";

        //    return RedirectToAction("Index", "Home");
        //}
    }
}

