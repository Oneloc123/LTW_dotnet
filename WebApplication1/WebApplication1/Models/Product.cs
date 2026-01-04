using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        [Column("name")]
        public string Name { get; set; } = "";

        [MaxLength(2000)]
        [Column("description")]
        public string Description { get; set; } = "";

        [Required, Range(0, double.MaxValue)]
        [Column("price")]
        public decimal Price { get; set; }

        [Required, MaxLength(500)]
        [Column("main_image_url")]
        public string MainImageUrl { get; set; } = "";

        [Required, MaxLength(100)]
        [Column("brand")]
        public string Brand { get; set; } = "";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

<<<<<<< HEAD
        public string Image { get; set; } = "";

        public List<ProductImage> Images { get; set; } = new List<ProductImage>();
        public List<Specification> Specifications { get; set; } = new List<Specification>();
        public List<Reviews> Reviews { get; set; } = new List<Reviews>();
=======
        // Navigation
        public List<Specification> Specifications { get; set; } = new();
        public List<Reviews> Reviews { get; set; } = new();
        public List<ProductVariant> ProductVariants { get; set; } = new();
>>>>>>> 16b8ce6228f999353b3fe3e5712299214bb452a7
    }
}