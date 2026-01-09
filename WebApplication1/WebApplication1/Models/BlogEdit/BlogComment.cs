using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.BlogEdit
{
    [Table("blog_comments")]
    public class BlogComment
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("blog_id")]
        public int BlogId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // Navigation (không map cột)
        public Blog Blog { get; set; }

        public WebApplication1.Models.UserEdit.User  User { get; set; }

    }

}
