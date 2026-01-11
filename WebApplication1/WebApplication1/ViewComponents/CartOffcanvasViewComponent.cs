using Microsoft.AspNetCore.Mvc;
using WebApplication1.Extension;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.ViewComponents
{
    // lớp bổ trợ tái sd giao diện của cart ở các nơi khác nhau => tránh lặp code
    public class CartOffcanvasViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = CartSessionHelper.GetCart(HttpContext);
            return View(cart);
        }
    }
}
