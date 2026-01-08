using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("orders")]
    public class Orders
    {
        //[Key]
        //public int Id { get; set; }

        //public int UserId { get; set; }

        //[Required, MaxLength(50)]
        //public string Status { get; set; } = "Pending";

        //[Column(TypeName = "decimal(18,2)")]
        //public decimal TotalAmount { get; set; }

        //public DateTime CreatedAt { get; set; } = DateTime.Now;

        //public List<OrderItem> OrderItems { get; set; } = new();
    }
}
