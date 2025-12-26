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
        public int Rating { get; set; } // 1 đến 5 sao

        [Column("review_comment")]
        public string Comment { get; set; } = "";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("product_id")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; } // nếu có bảng người dùng

        [NotMapped]
        public string UserName { get; set; } = "Khách ẩn danh";
    }
}