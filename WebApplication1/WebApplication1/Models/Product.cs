namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Brand { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<ProductImage> Images { get; set; } = new List<ProductImage>();
        public List<Specification> Specifications { get; set; } = new List<Specification>();
        public List<Reviews> Reviews { get; set; } = new List<Reviews>();
    }
}
