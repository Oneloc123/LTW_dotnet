using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models.BlogEdit
{
    [Table("blogs")]
    public class Blog
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        public WebApplication1.Models.UserEdit.User User { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("content")]
        public string? Content { get; set; }

        [Column("thumbnail")]
        public string? Thumbnail { get; set; }

        [Column("view_count")]
        public int ViewCount { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("is_approved")]
        public bool IsApproved { get; set; }
        public ICollection<BlogComment>? Comments { get; set; }
        public ICollection<BlogRating>? Ratings { get; set; }
    }

}
