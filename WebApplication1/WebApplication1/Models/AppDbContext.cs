using Microsoft.EntityFrameworkCore;


namespace WebApplication1.Models
{
    public class AppDbContext : DbContext
    {
        /*
         * Dòng này:
         * Nhận cấu hình kết nối DB từ Program.cs
         * Ví dụ connection string, provider (SQL Server, MySQL…)
         * Không có constructor này → app không kết nối DB được
         */
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        /*
         * Dòng QUAN TRỌNG NHẤT
            Ý nghĩa:
            usersnet = bảng trong database
            Users = class Entity
            EF Core tự hiểu:
            usersnet (table)  <-->  Users (class)
         */
        public DbSet<Users> usersnet { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
    }
}
