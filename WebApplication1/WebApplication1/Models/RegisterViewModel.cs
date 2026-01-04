using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [StringLength(20, MinimumLength = 6,
            ErrorMessage = "Tên đăng nhập từ 6–20 ký tự")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$",
            ErrorMessage = "Chỉ chứa chữ, số và dấu gạch dưới")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(100, MinimumLength = 8,
            ErrorMessage = "Mật khẩu ít nhất 8 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Range(typeof(bool), "true", "true",
            ErrorMessage = "Bạn phải đồng ý điều khoản")]
        public bool AcceptTerms { get; set; }
    }
}
