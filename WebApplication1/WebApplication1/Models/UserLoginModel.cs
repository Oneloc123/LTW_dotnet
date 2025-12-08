using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được trống")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
