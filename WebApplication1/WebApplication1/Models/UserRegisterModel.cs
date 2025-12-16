using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được trống")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email không được trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu nhập lại không đúng")]
        public string ConfirmPassword { get; set; }
    }
}
