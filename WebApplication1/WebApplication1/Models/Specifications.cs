using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("specifications")]
    public class Specification
    {
        [Key]
        public int Id { get; set; }

        public string SpecName { get; set; } = "";  
        public string SpecValue { get; set; } = "";

        public int ProductId { get; set; }
    }
}