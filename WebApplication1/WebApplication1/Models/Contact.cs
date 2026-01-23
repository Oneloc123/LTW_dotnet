using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("contacts")]
    public class Contact
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("full_name")]
        public string FullName { get; set; }

        [Required, EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Required]
        [Column("message")]
        public string Message { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("is_read")]
        public bool IsRead { get; set; } = false;
    }
}
