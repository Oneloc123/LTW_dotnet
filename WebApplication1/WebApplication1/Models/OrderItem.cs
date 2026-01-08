using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("order_items")]
    public class OrderItem
    {
        //[Key]
        //public int Id { get; set; }

        //public int OrderId { get; set; }
        //[ForeignKey("OrderId")]
        //public Orders Order { get; set; }

        //public int ProductId { get; set; }
        //[ForeignKey("ProductId")]
        //public Product Product { get; set; }

        //public int Quantity { get; set; }

        //[Column(TypeName = "decimal(18,2)")]
        //public decimal Price { get; set; }
    }
}
