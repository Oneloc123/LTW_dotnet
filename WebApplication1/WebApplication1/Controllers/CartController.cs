using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext db;

        public CartController(AppDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            Console.WriteLine("SESSION ID (Cart): " + HttpContext.Session.Id);
            var cart = CartSessionHelper.GetCart(HttpContext);

            if (!cart.Items.Any())
            {
                cart.AddOrUpdate(new CartItem
                {
                    VariantId = 1,
                    Name = "Xiaomi 15T Pro 5G 12GB 512GB Xám",
                    ImageUrl = "/images/xiaomi_15t_pro_xam_5_a006830687.webp",
                    Color = "Xám",
                    Memory = "512GB",
                    Price = 19490000,
                    Quantity = 1,
                    Discount = 0
                });

                CartSessionHelper.SaveCart(HttpContext, cart);
            }

            return View(cart);

        } // truyền dữ liệu từ controller sang view

        /* Thêm sản phẩm */
        [HttpPost]
        public IActionResult Add(int variantId, int quantity = 1)
        {
            if (quantity <= 0) quantity = 1;

            var variant = db.ProductVariants
                .Include(v => v.Product)
                .FirstOrDefault(v => v.Id == variantId);

            if (variant == null)
                return NotFound();

            var cart = CartSessionHelper.GetCart(HttpContext);
            cart.AddOrUpdate(new CartItem
            {
                VariantId = variant.Id,
                Name = variant.Product.Name,
                ImageUrl = variant.ImageUrl,
                Color = variant.Color,
                Memory = variant.Memory,
                Price = variant.Price,
                Quantity = quantity,
                Discount = 0
            });

            CartSessionHelper.SaveCart(HttpContext, cart);

            return Ok(new
            {
                totalQuantity = cart.TotalQuantity,
                totalPrice = cart.TotalMoney
            });
        }

        /* post -> tăng sản phẩm có id */
        [HttpPost]
        public IActionResult Increase(int variantId)
        {
            var cart = CartSessionHelper.GetCart(HttpContext);
            cart.Increase(variantId);
            CartSessionHelper.SaveCart(HttpContext, cart);
            return Ok();
        }

        /* post -> giảm sản phẩm có id */
        [HttpPost]
        public IActionResult Decrease(int variantId)
        {
            var cart = CartSessionHelper.GetCart(HttpContext);
            cart.Decrease(variantId);
            CartSessionHelper.SaveCart(HttpContext, cart);
            return Ok();
        }

        /* post -> xóa sản phẩm theo id  */
        [HttpPost]
        public IActionResult Remove(int variantId)
        {
            var cart = CartSessionHelper.GetCart(HttpContext);
            cart.Remove(variantId);
            CartSessionHelper.SaveCart(HttpContext, cart);
            return Ok();
        }

        /* Xóa nhiều cart item */
        [HttpPost]
        public IActionResult RemoveSelected([FromBody] List<int> variantIds)
        {
            var cart = CartSessionHelper.GetCart(HttpContext);

            foreach (var id in variantIds)
            {
                cart.Remove(id);
            }

            CartSessionHelper.SaveCart(HttpContext, cart);
            return Ok(new
            {
                totalQuantity = cart.TotalQuantity,
                totalPrice = cart.TotalMoney
            });
        }

        /* post -> xóa hết sản phẩm trong cart */
        [HttpPost]
        public IActionResult Clear()
        {
            CartSessionHelper.Clear(HttpContext);
            return Ok();
        }

        [HttpGet]
        public IActionResult Offcanvas()
        {
            var cart = CartSessionHelper.GetCart(HttpContext);
            return PartialView("~/Views/Shared/Components/CartOffcanvas/Default.cshtml", cart);
        }

        /* trả về tổng số lượng sản phẩm trong giỏ hàng => cập nhật số lượng trên đầu icon cart */
        [HttpGet]
        public IActionResult Count()
        {
            var cart = CartSessionHelper.GetCart(HttpContext);
            return Json(new { totalQuantity = cart.TotalQuantity });
        }

    }
}