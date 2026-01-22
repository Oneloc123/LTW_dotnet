using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.OrderEdit.Order
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("address_id")]
        public int? AddressId { get; set; }

        [Column("total_price")]
        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        [Column("order_status")]
        [StringLength(50)]
        public string? Status { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public void add(OrderItem orderItem)
        {
            this.OrderItems.Add(orderItem);
        }



        public string GetFirstImageUrl()
        {
            return OrderItems.FirstOrDefault()?.ImageUrl ?? "/images/no-image.png";
        }

        public string GetFirstProductName()
        {
            return OrderItems.FirstOrDefault()?.Name ?? "Sản phẩm";
        }

        public int GetRemainingItemCount()
        {
            return Math.Max(0, OrderItems.Count );
        }



    }

}
