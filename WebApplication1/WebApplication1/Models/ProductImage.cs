using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("product_images")]
    public class ProductImage
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; } = "";

        [Column("is_main")]
        public bool IsMain { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products Product { get; set; }
    }
}