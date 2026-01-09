using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.UserEdit
{
    [Table("usersnet")]
    public class Users
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_name")]
        public String user_Name { get; set; }
        public String email { get; set; }
        [Column("password")]
        public String passWord { get; set; }
        [Column("random_key")]
        public String randomKey { get; set; }
    }
}
