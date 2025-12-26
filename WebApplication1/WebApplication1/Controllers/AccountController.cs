using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
       
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
        
            return View();
        }
        // POST: /Account/LoginAjax (Xử lý Ajax)
       

        // ----------------------------- REGISTER -----------------------------

        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        
        
        //----------------------CHANGE PASSWORD----------------------
        // GET: Đổi mật khẩu
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: Đổi mật khẩu với Ajax
       
    }
}

