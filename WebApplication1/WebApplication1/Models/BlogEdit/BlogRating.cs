using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.BlogEdit
{
    [Table("blog_ratings")]
    public class BlogRating
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("blog_id")]
        public int BlogId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("rating")]
        public int Rating { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Blog Blog { get; set; }
    }

}
