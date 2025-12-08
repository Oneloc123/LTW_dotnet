using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("reviews")]
    public class Reviews
    {
        [Key]
        public int Id { get; set; }

        public int Rating { get; set; } // 1 đến 5 sao
        public string Comment { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        
        public int ProductId { get; set; }

        
        public int UserId { get; set; }

        [NotMapped]
        public string UserName { get; set; } = "Khách ẩn danh";
    }
}