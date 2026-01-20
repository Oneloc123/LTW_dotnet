using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("products")]
    public class Products
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; } = "";

        [Column("description")]
        public string Description { get; set; } = "";

        [Required, Range(0, double.MaxValue)]
        [Column("price")]
        public decimal Price { get; set; }

        [Column("image")]
        [MaxLength(500)]
        public string MainImageUrl { get; set; } = "";

        [Required, MaxLength(100)]
        [Column("brand")]
        public string Brand { get; set; } = "";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation
        public List<ProductImage> Images { get; set; } = new List<ProductImage>();
        public List<Specification> Specifications { get; set; } = new();
        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
        public List<ProductVariant> ProductVariants { get; set; } = new();


    }
}
