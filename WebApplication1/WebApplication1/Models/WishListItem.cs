using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace WebApplication1.Models.UserEdit
{
    [Table("wishListItem")]
    public class WishListItem
    {
        [Key]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // navigation
        public User User { get; set; }
        public Products Product { get; set; }
    }
}
