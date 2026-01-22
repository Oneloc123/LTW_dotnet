namespace WebApplication1.Models.UserEdit
{
    public class UserEditVM
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;
        public string? FullName { get; set; }
        public string Role { get; set; } = "User";
        public bool IsActive { get; set; }
    }
}
