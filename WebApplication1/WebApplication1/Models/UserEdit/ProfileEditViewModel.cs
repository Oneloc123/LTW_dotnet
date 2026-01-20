using WebApplication1.Models.OrderEdit.Order;

namespace WebApplication1.Models.UserEdit
{
    public class ProfileEditViewModel
    {
        // User
        public string FullName { get; set; }
        public String Email { get; set; }

        public String Username { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // Address
        public int? AddressId { get; set; }
        public string FullAddress { get; set; }

        // Avatar
        public IFormFile AvatarFile { get; set; }   // 👈 upload
        public string CurrentAvatarUrl { get; set; } // 👈 hiển thị ảnh cũ

        public List<BlogEdit.Blog> Blogs { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
       // public List<Review> Reviews { get; set; } = new();
    }
}

