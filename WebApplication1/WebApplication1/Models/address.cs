using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("user_addresses")]
    public class UserAddress
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [StringLength(500)]
        [Column("full_address")]
        public string FullAddress { get; set; }

        // ======================
        // Navigation (optional)
        // ======================
        //[ForeignKey("UserId")]
        //public User User { get; set; }
    }
}
