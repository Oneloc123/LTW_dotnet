using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.Checkout;
using WebApplication1.Models.Enum;
using WebApplication1.Models.OrderEdit.Order;
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

            CartDiscountType discountType = CartDiscountType.None;
            decimal discountValue = 0;

            if (discount != null)
            {
                if (discount.Type == DiscountType.Percentage)
                {
                    discountType = CartDiscountType.Percentage;
                    discountValue = discount.Value;
                }
                else
                {
                    discountType = CartDiscountType.Amount;
                    discountValue = discount.Value;
                }
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
                Quantity = quantity,

                DiscountType = discountType,
                DiscountValue = discountValue,

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

            // chặn áp dụng voucher khi cart rỗng
            if (!cart.Items.Any())
                return BadRequest(new { message = "Giỏ hàng trống, không thể áp dụng voucher" });

            var voucher = db.Discounts
                .Where(d => d.IsActive && d.ProductId == null &&
                            d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now &&
                            d.Name == code)
                .OrderByDescending(d => d.Value)
                .FirstOrDefault();

            if (voucher == null)
                return BadRequest(new { message = "Voucher không hợp lệ" });

            if (voucher.Type != DiscountType.Percentage)
                return BadRequest(new { message = "Hiện chỉ hỗ trợ voucher %" });

            cart.ApplyVoucherPercent(voucher.Value);
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
            // recalc voucher nếu có % 
            if (cart.VoucherPercent > 0)
            {
                cart.ApplyVoucherPercent(cart.VoucherPercent);
            }
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
            // recalc voucher nếu có % 
            if (cart.VoucherPercent > 0)
            {
                cart.ApplyVoucherPercent(cart.VoucherPercent);
            }
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
            return Ok(new
            {
                totalQuantity = cart.TotalQuantity,
                totalMoney = cart.TotalMoney,
                totalDiscount = cart.TotalDiscount,
                voucher = cart.TotalVoucherDiscount,
                totalPayable = cart.TotalPayable
            });
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
                totalMoney = cart.TotalMoney,
                totalDiscount = cart.TotalDiscount,
                voucher = cart.TotalVoucherDiscount,
                totalPayable = cart.TotalPayable
            });
        }
        [HttpPost]
        public IActionResult PlaceOrder(Checkout model)
        {
            // 1️⃣ Validate cơ bản
            if (!ModelState.IsValid)
                return View("Index", model);

            var cart = CartSessionHelper.GetCart(HttpContext);
            if (!cart.Items.Any())
                return RedirectToAction("Index", "Cart");

            // 2️⃣ Lấy user từ session
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // 3️⃣ Tạo ORDER
            var order = new Order
            {
                UserId = userId.Value,
                AddressId = model.AddressId,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            db.Orders.Add(order);
            db.SaveChanges(); // 🔥 BẮT BUỘC để có order.Id

            // 4️⃣ Tạo ORDER ITEMS (THEO VARIANT)
            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    VariantId = item.VariantId,
                    Quantity = item.Quantity,
                    //Price = item.FinalPrice // đóng băng giá lúc mua
                };

                db.OrderItems.Add(orderItem);
            }

            db.SaveChanges();

            // 5️⃣ Clear cart
            CartSessionHelper.Clear(HttpContext);

            // 6️⃣ Redirect sang trang chi tiết đơn hàng
            return RedirectToAction("Details", "Order", new { id = order.Id });
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