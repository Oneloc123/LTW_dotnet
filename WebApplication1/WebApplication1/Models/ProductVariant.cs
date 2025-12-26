using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace WebApplication1.Models
{
    [Table("product_variants")]
    public class ProductVariant
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("product_id")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required, MaxLength(50)]
        [Column("color")]
        public string Color { get; set; } = "";

        [Required, MaxLength(50)]
        [Column("memory")]
        public string Memory { get; set; } = "";

        [Required, Range(0, double.MaxValue)]
        [Column("price")]
        public decimal Price { get; set; }

        [Required, Range(0, int.MaxValue)]
        [Column("stock")]
        public int Stock { get; set; }

        [Required, MaxLength(500)]
        [Column("image_url")]
        public string ImageUrl { get; set; } = "";
    }
}
