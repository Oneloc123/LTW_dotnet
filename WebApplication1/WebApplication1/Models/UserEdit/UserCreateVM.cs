namespace WebApplication1.Models.UserEdit
{
    public class UserCreateVM
    {
        
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string? FullName { get; set; }
        public string Role { get; set; } = "User";
        public bool IsActive { get; set; } = true;
    }
}

