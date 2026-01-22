using WebApplication1.Models.User.User;

namespace WebApplication1.Models.Checkout
{
    public class Checkout
    {
        public List<CheckoutItem> Items { get; set; }

        public decimal TotalMoney { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalPayable { get; set; }

        // Thông tin khách hàng
        public string FullName { get; set; }
        public string Phone { get; set; }
        public int AddressId { get; set; }

        public String PaymentMethod { get; set; }

        public Carts Cart { get; set; }
        public List<UserAddress> Addresses { get; set; }
    }
}
