using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.UserEdit;

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
            /* demo */
            //if (!cart.Items.Any())
            //{
            //    // Lấy variant đầu tiên trong DB để demo
            //    var variant = db.ProductVariants
            //        .Include(v => v.Product)
            //        .FirstOrDefault();

            //    if (variant != null)
            //    {
            //        cart.AddOrUpdate(new CartItem
            //        {
            //            VariantId = variant.Id,
            //            Name = variant.Product.Name,
            //            ImageUrl = !string.IsNullOrEmpty(variant.ImageUrl)
            //                        ? variant.ImageUrl
            //                        : variant.Product.MainImageUrl,
            //            Color = variant.Color,
            //            Memory = variant.Memory,
            //            Price = variant.Price,
            //            Quantity = 1,
            //            Discount = 0,
            //            AvailableColors = variant.Product.ProductVariants
            //                .Select(v => (v.Color, v.ImageUrl ?? variant.Product.MainImageUrl))
            //                .ToList()
            //        });

            //        CartSessionHelper.SaveCart(HttpContext, cart);
            //    }
            //}

            return View(cart);
        }
        // truyền dữ liệu từ controller sang view

        /* Thêm sản phẩm */
        [HttpPost]
        public IActionResult Add(int variantId, int quantity = 1)
        {
            if (quantity <= 0) quantity = 1;

            var variant = db.ProductVariants
                .Include(v => v.Product)
                .FirstOrDefault(v => v.Id == variantId); // Dùng variantId từ client

            if (variant == null)
                return NotFound(); // không tìm thấy variant

            // Lấy chương trình giảm giá đang active
            var discount = db.Discounts
                .Where(d => d.IsActive &&
                            (d.ProductId == null || d.ProductId == variant.ProductId) &&
                            d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now)
                .OrderByDescending(d => d.Value) // giảm cao nhất
                .FirstOrDefault();

            decimal discountAmount = 0;
            int discountPercent = 0;

            if (discount != null)
            {
                if (discount.Type == DiscountType.Percentage)
                    discountPercent = (int)discount.Value;
                else
                    discountAmount = discount.Value;
            }

            var cart = CartSessionHelper.GetCart(HttpContext);

            cart.AddOrUpdate(new CartItem
            {
                VariantId = variant.Id,
                Name = variant.Product.Name,
                ImageUrl = !string.IsNullOrEmpty(variant.ImageUrl)
                            ? variant.ImageUrl
                            : variant.Product.MainImageUrl,
                Color = variant.Color,
                Memory = variant.Memory,
                Price = variant.Price,
                Quantity = quantity, // lấy số lượng từ client
                Discount = discountPercent,
                DiscountAmount = discountAmount,
                AvailableColors = variant.Product.ProductVariants
                        .Select(v => (v.Color, v.ImageUrl ?? variant.Product.MainImageUrl))
                        .ToList()
            });

            CartSessionHelper.SaveCart(HttpContext, cart);

            return Ok(new
            {
                totalQuantity = cart.TotalQuantity,
                totalPrice = cart.TotalPayable, // đã tính cả voucher
                totalDiscount = cart.TotalDiscount,
                voucher = cart.TotalVoucherDiscount
            });
        }

        [HttpPost]
        public IActionResult ApplyVoucher(string code)
        {
            var cart = CartSessionHelper.GetCart(HttpContext);

            var voucher = db.Discounts
                .Where(d => d.IsActive && d.ProductId == null &&
                            d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now &&
                            d.Name == code)
                .OrderByDescending(d => d.Value)
                .FirstOrDefault();

            if (voucher == null)
                return BadRequest(new { message = "Voucher không hợp lệ" });

            decimal discountAmount = 0;
            if (voucher.Type == DiscountType.Percentage)
                discountAmount = cart.TotalMoney * (voucher.Value / 100m);
            else
                discountAmount = voucher.Value;

            cart.ApplyVoucher(discountAmount);
            CartSessionHelper.SaveCart(HttpContext, cart);

            return Ok(new
            {
                totalQuantity = cart.TotalQuantity,
                totalMoney = cart.TotalMoney,
                totalDiscount = cart.TotalDiscount,
                voucher = cart.TotalVoucherDiscount,
                totalPayable = cart.TotalPayable
            });
        }

        /* post -> tăng sản phẩm có id */
        [HttpPost]
        public IActionResult Increase(int variantId)
        {
            var cart = CartSessionHelper.GetCart(HttpContext);
            cart.Increase(variantId);
            CartSessionHelper.SaveCart(HttpContext, cart);
            // Trả JSON cart đầy đủ để JS cập nhật
            return Ok(new
            {
                items = cart.Items.Select(i => new
                {
                    variantId = i.VariantId,
                    quantity = i.Quantity,
                    finalPrice = i.FinalPrice
                }),
                totalQuantity = cart.TotalQuantity,
                totalMoney = cart.TotalMoney,
                totalDiscount = cart.TotalDiscount,
                voucher = cart.TotalVoucherDiscount,
                totalPayable = cart.TotalPayable
            });
        }

        /* post -> giảm sản phẩm có id */
        [HttpPost]
        public IActionResult Decrease(int variantId)
        {
            var cart = CartSessionHelper.GetCart(HttpContext);
            cart.Decrease(variantId);
            CartSessionHelper.SaveCart(HttpContext, cart);
            return Ok(new
            {
                items = cart.Items.Select(i => new
                {
                    variantId = i.VariantId,
                    quantity = i.Quantity,
                    finalPrice = i.FinalPrice
                }),
                totalQuantity = cart.TotalQuantity,
                totalMoney = cart.TotalMoney,
                totalDiscount = cart.TotalDiscount,
                voucher = cart.TotalVoucherDiscount,
                totalPayable = cart.TotalPayable
            });
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