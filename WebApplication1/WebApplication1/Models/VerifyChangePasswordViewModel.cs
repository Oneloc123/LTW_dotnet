using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class VerifyChangePasswordViewModel
    {
        [Required]
        public string Otp { get; set; }
    }
}
