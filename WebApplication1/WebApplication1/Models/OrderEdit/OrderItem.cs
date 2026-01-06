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
        [Column("quantity")]
        public int Quantity { get; set; }

        // ===== Navigation Properties (optional) =====
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        // Nếu có bảng Product
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
