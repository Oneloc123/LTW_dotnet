using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.OrderEdit.Order
{
    [Table("orderitems")]
    public class OrderItem
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Required]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Required]
        [Column("variant_id")]
        public int VariantId { get; set; }

        [Required]
        [Column("product_name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("product_image")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Column("price")]
        public decimal Price { get; set; }

        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }


        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // ===== Navigation Properties (optional) =====
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

        // Nếu có bảng Product
        [ForeignKey("ProductId")]
        public Products? Product { get; set; }
    }
}
