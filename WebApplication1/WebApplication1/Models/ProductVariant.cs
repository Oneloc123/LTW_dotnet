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
        public Products Product { get; set; }

        [Required, MaxLength(50)]
        [Column("color")]
        public string Color { get; set; } = "";

        [Required, MaxLength(50)]
        [Column("memory")]
        public string Memory { get; set; } = "";

        [Required]
        [Column("price")]
        public decimal Price { get; set; }

        [Required]
        [Column("stock")]
        public int Stock { get; set; }

        [Column("image_url")]
        public string? ImageUrl { get; set; }
    }
}
