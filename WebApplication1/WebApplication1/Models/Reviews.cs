using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("reviews")]
    public class Reviews
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Range(1, 5)]
        [Column("rating")]
        public int Rating { get; set; }

        [Column("review_comment")]
        public string Comment { get; set; } = "";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products Product { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("user_name")]
        public string? UserName { get; set; }
    }
}