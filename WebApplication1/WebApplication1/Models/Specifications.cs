using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("specifications")]
    public class Specification
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("spec_name")]
        public string SpecName { get; set; } = "";  // Ví dụ: "RAM"

        [Column("spec_value")]
        public string SpecValue { get; set; } = ""; // Ví dụ: "8GB"

        [Column("product_id")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; } // Quan hệ navigation
    }
}