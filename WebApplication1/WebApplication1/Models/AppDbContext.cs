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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(u => u.Role)
                      .HasDefaultValue("User");

                entity.Property(u => u.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }

        public DbSet<Users> usersnet { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
