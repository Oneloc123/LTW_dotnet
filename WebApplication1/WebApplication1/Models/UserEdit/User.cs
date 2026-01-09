using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.BlogEdit;

namespace WebApplication1.Models.UserEdit
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        // ======================
        // Thông tin đăng nhập
        // ======================

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        // ======================
        // Thông tin cá nhân
        // ======================

        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        // 0 = Unknown, 1 = Male, 2 = Female
        public int Gender { get; set; } = 0;

        [StringLength(255)]
        public string AvatarUrl { get; set; }

        // ======================
        // Trạng thái & bảo mật
        // ======================

        public bool IsActive { get; set; } = true;

        public bool EmailConfirmed { get; set; } = false;

        public int FailedLoginCount { get; set; } = 0;

        public DateTime? LockoutEnd { get; set; }

        public DateTime? LastLoginAt { get; set; }

        // ======================
        // Quyền & hệ thống
        // ======================

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = "User";
        // Admin | User | Manager ...

        // ======================
        // Audit
        // ======================

        public DateTime CreatedAt { get; set; } 

        public DateTime? UpdatedAt { get; set; }

        public ICollection<Blog> Blogs { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }


    }
}
