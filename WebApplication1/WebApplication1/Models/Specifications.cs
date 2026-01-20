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
        public string SpecName { get; set; } = "";

        [Column("spec_value")]
        public string SpecValue { get; set; } = "";

        [Column("product_id")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products Product { get; set; }
    }
}