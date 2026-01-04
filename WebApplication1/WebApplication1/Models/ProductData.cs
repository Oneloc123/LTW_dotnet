using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace WebApplication1.Models
{
    public class ProductData
    {
        
        public static List<Product> GetProducts(string connectionString, string search, string brand)
        {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            List<Product> list = new List<Product>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                
                string query = "SELECT * FROM products WHERE 1=1";

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND name LIKE @search";
                }

                
                if (!string.IsNullOrEmpty(brand))
                {
                    query += " AND brand = @brand";
                }

                MySqlCommand cmd = new MySqlCommand(query, conn);

                if (!string.IsNullOrEmpty(search)) cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                
                if (!string.IsNullOrEmpty(brand)) cmd.Parameters.AddWithValue("@brand", brand);
=======
//            _products = new List<Product>()
//            {
//    new Product {
//        Id = 10,
//        Name = "iPhone 15 Pro Max",
//        Brand = "Apple",
//        Price = 28990000,
//        Description = "Flagship mạnh mẽ của Apple với chip A17 Pro, camera 48MP.",
//        Stock = 12,
//        CreatedAt = DateTime.Now.AddDays(-2),

//        Images = new List<ProductImage>
//        {
//            new ProductImage { Id=21, ProductId=10, ImageUrl="/images/iphone15promax.jpg", IsMain=true }
//        },

//        Specifications = new List<Specification>
//        {
//            new Specification { Id=41, ProductId=10, SpecName="Màn hình", SpecValue="6.7 inch OLED" },
//            new Specification { Id=42, ProductId=10, SpecName="Chip", SpecValue="A17 Pro" },
//            new Specification { Id=43, ProductId=10, SpecName="Camera", SpecValue="48MP + 12MP + 12MP" },
//            new Specification { Id=44, ProductId=10, SpecName="Pin", SpecValue="4422 mAh" }
//        },

//        Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 11,
//    Name = "Samsung Galaxy S24 Ultra",
//    Brand = "Samsung",
//    Price = 25990000,
//    Description = "Điện thoại Android mạnh nhất với camera 200MP, S Pen.",
//    Stock = 18,
//    CreatedAt = DateTime.Now.AddDays(-6),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=22, ProductId=11, ImageUrl="/images/s24ultra.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=45, ProductId=11, SpecName="Màn hình", SpecValue="6.8 inch Dynamic AMOLED 2X" },
//        new Specification { Id=46, ProductId=11, SpecName="Chip", SpecValue="Snapdragon 8 Gen 3" },
//        new Specification { Id=47, ProductId=11, SpecName="Camera", SpecValue="200MP + 50MP + 10MP + 12MP" },
//        new Specification { Id=48, ProductId=11, SpecName="Pin", SpecValue="5000 mAh" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 12,
//    Name = "Xiaomi Redmi Note 13 Pro",
//    Brand = "Xiaomi",
//    Price = 7490000,
//    Description = "Điện thoại tầm trung camera 200MP, sạc nhanh 67W.",
//    Stock = 20,
//    CreatedAt = DateTime.Now.AddDays(-10),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=23, ProductId=12, ImageUrl="/images/redmi13pro.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=49, ProductId=12, SpecName="Màn hình", SpecValue="6.67 inch AMOLED 120Hz" },
//        new Specification { Id=50, ProductId=12, SpecName="Chip", SpecValue="Snapdragon 7s Gen 2" },
//        new Specification { Id=51, ProductId=12, SpecName="Camera", SpecValue="200MP + 8MP + 2MP" },
//        new Specification { Id=52, ProductId=12, SpecName="Pin", SpecValue="5100 mAh, 67W" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 13,
//    Name = "OPPO Reno 11 Pro",
//    Brand = "OPPO",
//    Price = 13990000,
//    Description = "Thiết kế đẹp, camera chân dung 50MP, hiệu năng ổn định.",
//    Stock = 22,
//    CreatedAt = DateTime.Now.AddDays(-3),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=24, ProductId=13, ImageUrl="/images/reno11pro.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=53, ProductId=13, SpecName="Màn hình", SpecValue="6.7 inch AMOLED 120Hz" },
//        new Specification { Id=54, ProductId=13, SpecName="Chip", SpecValue="Dimensity 8200" },
//        new Specification { Id=55, ProductId=13, SpecName="Camera", SpecValue="50MP + 8MP + 32MP" },
//        new Specification { Id=56, ProductId=13, SpecName="Pin", SpecValue="4600 mAh, sạc 80W" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 14,
//    Name = "Realme 11 Pro+",
//    Brand = "Realme",
//    Price = 10990000,
//    Description = "Camera 200MP, thiết kế mặt lưng da độc đáo.",
//    Stock = 15,
//    CreatedAt = DateTime.Now.AddDays(-12),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=25, ProductId=14, ImageUrl="/images/realme11pro.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=57, ProductId=14, SpecName="Màn hình", SpecValue="6.7 inch AMOLED 120Hz" },
//        new Specification { Id=58, ProductId=14, SpecName="Chip", SpecValue="Dimensity 7050" },
//        new Specification { Id=59, ProductId=14, SpecName="Camera", SpecValue="200MP + 8MP + 2MP" },
//        new Specification { Id=60, ProductId=14, SpecName="Pin", SpecValue="5000 mAh, 100W" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 15,
//    Name = "Vivo V30",
//    Brand = "Vivo",
//    Price = 11990000,
//    Description = "Camera chụp chân dung đẹp, hiệu năng mượt, pin lớn.",
//    Stock = 17,
//    CreatedAt = DateTime.Now.AddDays(-4),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=26, ProductId=15, ImageUrl="/images/v30.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=61, ProductId=15, SpecName="Màn hình", SpecValue="6.78 inch AMOLED 120Hz" },
//        new Specification { Id=62, ProductId=15, SpecName="Chip", SpecValue="Snapdragon 7 Gen 3" },
//        new Specification { Id=63, ProductId=15, SpecName="Camera", SpecValue="50MP + 50MP" },
//        new Specification { Id=64, ProductId=15, SpecName="Pin", SpecValue="5000 mAh, 80W" }
//    },

=======
//            _products = new List<Product>()
//            {
//    new Product {
//        Id = 10,
//        Name = "iPhone 15 Pro Max",
//        Brand = "Apple",
//        Price = 28990000,
//        Description = "Flagship mạnh mẽ của Apple với chip A17 Pro, camera 48MP.",
//        Stock = 12,
//        CreatedAt = DateTime.Now.AddDays(-2),

//        Images = new List<ProductImage>
//        {
//            new ProductImage { Id=21, ProductId=10, ImageUrl="/images/iphone15promax.jpg", IsMain=true }
//        },

//        Specifications = new List<Specification>
//        {
//            new Specification { Id=41, ProductId=10, SpecName="Màn hình", SpecValue="6.7 inch OLED" },
//            new Specification { Id=42, ProductId=10, SpecName="Chip", SpecValue="A17 Pro" },
//            new Specification { Id=43, ProductId=10, SpecName="Camera", SpecValue="48MP + 12MP + 12MP" },
//            new Specification { Id=44, ProductId=10, SpecName="Pin", SpecValue="4422 mAh" }
//        },

//        Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 11,
//    Name = "Samsung Galaxy S24 Ultra",
//    Brand = "Samsung",
//    Price = 25990000,
//    Description = "Điện thoại Android mạnh nhất với camera 200MP, S Pen.",
//    Stock = 18,
//    CreatedAt = DateTime.Now.AddDays(-6),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=22, ProductId=11, ImageUrl="/images/s24ultra.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=45, ProductId=11, SpecName="Màn hình", SpecValue="6.8 inch Dynamic AMOLED 2X" },
//        new Specification { Id=46, ProductId=11, SpecName="Chip", SpecValue="Snapdragon 8 Gen 3" },
//        new Specification { Id=47, ProductId=11, SpecName="Camera", SpecValue="200MP + 50MP + 10MP + 12MP" },
//        new Specification { Id=48, ProductId=11, SpecName="Pin", SpecValue="5000 mAh" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 12,
//    Name = "Xiaomi Redmi Note 13 Pro",
//    Brand = "Xiaomi",
//    Price = 7490000,
//    Description = "Điện thoại tầm trung camera 200MP, sạc nhanh 67W.",
//    Stock = 20,
//    CreatedAt = DateTime.Now.AddDays(-10),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=23, ProductId=12, ImageUrl="/images/redmi13pro.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=49, ProductId=12, SpecName="Màn hình", SpecValue="6.67 inch AMOLED 120Hz" },
//        new Specification { Id=50, ProductId=12, SpecName="Chip", SpecValue="Snapdragon 7s Gen 2" },
//        new Specification { Id=51, ProductId=12, SpecName="Camera", SpecValue="200MP + 8MP + 2MP" },
//        new Specification { Id=52, ProductId=12, SpecName="Pin", SpecValue="5100 mAh, 67W" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 13,
//    Name = "OPPO Reno 11 Pro",
//    Brand = "OPPO",
//    Price = 13990000,
//    Description = "Thiết kế đẹp, camera chân dung 50MP, hiệu năng ổn định.",
//    Stock = 22,
//    CreatedAt = DateTime.Now.AddDays(-3),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=24, ProductId=13, ImageUrl="/images/reno11pro.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=53, ProductId=13, SpecName="Màn hình", SpecValue="6.7 inch AMOLED 120Hz" },
//        new Specification { Id=54, ProductId=13, SpecName="Chip", SpecValue="Dimensity 8200" },
//        new Specification { Id=55, ProductId=13, SpecName="Camera", SpecValue="50MP + 8MP + 32MP" },
//        new Specification { Id=56, ProductId=13, SpecName="Pin", SpecValue="4600 mAh, sạc 80W" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 14,
//    Name = "Realme 11 Pro+",
//    Brand = "Realme",
//    Price = 10990000,
//    Description = "Camera 200MP, thiết kế mặt lưng da độc đáo.",
//    Stock = 15,
//    CreatedAt = DateTime.Now.AddDays(-12),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=25, ProductId=14, ImageUrl="/images/realme11pro.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=57, ProductId=14, SpecName="Màn hình", SpecValue="6.7 inch AMOLED 120Hz" },
//        new Specification { Id=58, ProductId=14, SpecName="Chip", SpecValue="Dimensity 7050" },
//        new Specification { Id=59, ProductId=14, SpecName="Camera", SpecValue="200MP + 8MP + 2MP" },
//        new Specification { Id=60, ProductId=14, SpecName="Pin", SpecValue="5000 mAh, 100W" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 15,
//    Name = "Vivo V30",
//    Brand = "Vivo",
//    Price = 11990000,
//    Description = "Camera chụp chân dung đẹp, hiệu năng mượt, pin lớn.",
//    Stock = 17,
//    CreatedAt = DateTime.Now.AddDays(-4),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=26, ProductId=15, ImageUrl="/images/v30.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=61, ProductId=15, SpecName="Màn hình", SpecValue="6.78 inch AMOLED 120Hz" },
//        new Specification { Id=62, ProductId=15, SpecName="Chip", SpecValue="Snapdragon 7 Gen 3" },
//        new Specification { Id=63, ProductId=15, SpecName="Camera", SpecValue="50MP + 50MP" },
//        new Specification { Id=64, ProductId=15, SpecName="Pin", SpecValue="5000 mAh, 80W" }
//    },

>>>>>>> 16b8ce6228f999353b3fe3e5712299214bb452a7
=======
//            _products = new List<Product>()
//            {
//    new Product {
//        Id = 10,
//        Name = "iPhone 15 Pro Max",
//        Brand = "Apple",
//        Price = 28990000,
//        Description = "Flagship mạnh mẽ của Apple với chip A17 Pro, camera 48MP.",
//        Stock = 12,
//        CreatedAt = DateTime.Now.AddDays(-2),

//        Images = new List<ProductImage>
//        {
//            new ProductImage { Id=21, ProductId=10, ImageUrl="/images/iphone15promax.jpg", IsMain=true }
//        },

//        Specifications = new List<Specification>
//        {
//            new Specification { Id=41, ProductId=10, SpecName="Màn hình", SpecValue="6.7 inch OLED" },
//            new Specification { Id=42, ProductId=10, SpecName="Chip", SpecValue="A17 Pro" },
//            new Specification { Id=43, ProductId=10, SpecName="Camera", SpecValue="48MP + 12MP + 12MP" },
//            new Specification { Id=44, ProductId=10, SpecName="Pin", SpecValue="4422 mAh" }
//        },

//        Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 11,
//    Name = "Samsung Galaxy S24 Ultra",
//    Brand = "Samsung",
//    Price = 25990000,
//    Description = "Điện thoại Android mạnh nhất với camera 200MP, S Pen.",
//    Stock = 18,
//    CreatedAt = DateTime.Now.AddDays(-6),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=22, ProductId=11, ImageUrl="/images/s24ultra.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=45, ProductId=11, SpecName="Màn hình", SpecValue="6.8 inch Dynamic AMOLED 2X" },
//        new Specification { Id=46, ProductId=11, SpecName="Chip", SpecValue="Snapdragon 8 Gen 3" },
//        new Specification { Id=47, ProductId=11, SpecName="Camera", SpecValue="200MP + 50MP + 10MP + 12MP" },
//        new Specification { Id=48, ProductId=11, SpecName="Pin", SpecValue="5000 mAh" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 12,
//    Name = "Xiaomi Redmi Note 13 Pro",
//    Brand = "Xiaomi",
//    Price = 7490000,
//    Description = "Điện thoại tầm trung camera 200MP, sạc nhanh 67W.",
//    Stock = 20,
//    CreatedAt = DateTime.Now.AddDays(-10),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=23, ProductId=12, ImageUrl="/images/redmi13pro.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=49, ProductId=12, SpecName="Màn hình", SpecValue="6.67 inch AMOLED 120Hz" },
//        new Specification { Id=50, ProductId=12, SpecName="Chip", SpecValue="Snapdragon 7s Gen 2" },
//        new Specification { Id=51, ProductId=12, SpecName="Camera", SpecValue="200MP + 8MP + 2MP" },
//        new Specification { Id=52, ProductId=12, SpecName="Pin", SpecValue="5100 mAh, 67W" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 13,
//    Name = "OPPO Reno 11 Pro",
//    Brand = "OPPO",
//    Price = 13990000,
//    Description = "Thiết kế đẹp, camera chân dung 50MP, hiệu năng ổn định.",
//    Stock = 22,
//    CreatedAt = DateTime.Now.AddDays(-3),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=24, ProductId=13, ImageUrl="/images/reno11pro.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=53, ProductId=13, SpecName="Màn hình", SpecValue="6.7 inch AMOLED 120Hz" },
//        new Specification { Id=54, ProductId=13, SpecName="Chip", SpecValue="Dimensity 8200" },
//        new Specification { Id=55, ProductId=13, SpecName="Camera", SpecValue="50MP + 8MP + 32MP" },
//        new Specification { Id=56, ProductId=13, SpecName="Pin", SpecValue="4600 mAh, sạc 80W" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 14,
//    Name = "Realme 11 Pro+",
//    Brand = "Realme",
//    Price = 10990000,
//    Description = "Camera 200MP, thiết kế mặt lưng da độc đáo.",
//    Stock = 15,
//    CreatedAt = DateTime.Now.AddDays(-12),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=25, ProductId=14, ImageUrl="/images/realme11pro.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=57, ProductId=14, SpecName="Màn hình", SpecValue="6.7 inch AMOLED 120Hz" },
//        new Specification { Id=58, ProductId=14, SpecName="Chip", SpecValue="Dimensity 7050" },
//        new Specification { Id=59, ProductId=14, SpecName="Camera", SpecValue="200MP + 8MP + 2MP" },
//        new Specification { Id=60, ProductId=14, SpecName="Pin", SpecValue="5000 mAh, 100W" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 15,
//    Name = "Vivo V30",
//    Brand = "Vivo",
//    Price = 11990000,
//    Description = "Camera chụp chân dung đẹp, hiệu năng mượt, pin lớn.",
//    Stock = 17,
//    CreatedAt = DateTime.Now.AddDays(-4),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=26, ProductId=15, ImageUrl="/images/v30.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=61, ProductId=15, SpecName="Màn hình", SpecValue="6.78 inch AMOLED 120Hz" },
//        new Specification { Id=62, ProductId=15, SpecName="Chip", SpecValue="Snapdragon 7 Gen 3" },
//        new Specification { Id=63, ProductId=15, SpecName="Camera", SpecValue="50MP + 50MP" },
//        new Specification { Id=64, ProductId=15, SpecName="Pin", SpecValue="5000 mAh, 80W" }
//    },

>>>>>>> 16b8ce6228f999353b3fe3e5712299214bb452a7
=======
//            _products = new List<Product>()
//            {
//    new Product {
//        Id = 10,
//        Name = "iPhone 15 Pro Max",
//        Brand = "Apple",
//        Price = 28990000,
//        Description = "Flagship mạnh mẽ của Apple với chip A17 Pro, camera 48MP.",
//        Stock = 12,
//        CreatedAt = DateTime.Now.AddDays(-2),

//        Images = new List<ProductImage>
//        {
//            new ProductImage { Id=21, ProductId=10, ImageUrl="/images/iphone15promax.jpg", IsMain=true }
//        },

//        Specifications = new List<Specification>
//        {
//            new Specification { Id=41, ProductId=10, SpecName="Màn hình", SpecValue="6.7 inch OLED" },
//            new Specification { Id=42, ProductId=10, SpecName="Chip", SpecValue="A17 Pro" },
//            new Specification { Id=43, ProductId=10, SpecName="Camera", SpecValue="48MP + 12MP + 12MP" },
//            new Specification { Id=44, ProductId=10, SpecName="Pin", SpecValue="4422 mAh" }
//        },

//        Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 11,
//    Name = "Samsung Galaxy S24 Ultra",
//    Brand = "Samsung",
//    Price = 25990000,
//    Description = "Điện thoại Android mạnh nhất với camera 200MP, S Pen.",
//    Stock = 18,
//    CreatedAt = DateTime.Now.AddDays(-6),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=22, ProductId=11, ImageUrl="/images/s24ultra.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=45, ProductId=11, SpecName="Màn hình", SpecValue="6.8 inch Dynamic AMOLED 2X" },
//        new Specification { Id=46, ProductId=11, SpecName="Chip", SpecValue="Snapdragon 8 Gen 3" },
//        new Specification { Id=47, ProductId=11, SpecName="Camera", SpecValue="200MP + 50MP + 10MP + 12MP" },
//        new Specification { Id=48, ProductId=11, SpecName="Pin", SpecValue="5000 mAh" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 12,
//    Name = "Xiaomi Redmi Note 13 Pro",
//    Brand = "Xiaomi",
//    Price = 7490000,
//    Description = "Điện thoại tầm trung camera 200MP, sạc nhanh 67W.",
//    Stock = 20,
//    CreatedAt = DateTime.Now.AddDays(-10),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=23, ProductId=12, ImageUrl="/images/redmi13pro.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=49, ProductId=12, SpecName="Màn hình", SpecValue="6.67 inch AMOLED 120Hz" },
//        new Specification { Id=50, ProductId=12, SpecName="Chip", SpecValue="Snapdragon 7s Gen 2" },
//        new Specification { Id=51, ProductId=12, SpecName="Camera", SpecValue="200MP + 8MP + 2MP" },
//        new Specification { Id=52, ProductId=12, SpecName="Pin", SpecValue="5100 mAh, 67W" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 13,
//    Name = "OPPO Reno 11 Pro",
//    Brand = "OPPO",
//    Price = 13990000,
//    Description = "Thiết kế đẹp, camera chân dung 50MP, hiệu năng ổn định.",
//    Stock = 22,
//    CreatedAt = DateTime.Now.AddDays(-3),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=24, ProductId=13, ImageUrl="/images/reno11pro.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=53, ProductId=13, SpecName="Màn hình", SpecValue="6.7 inch AMOLED 120Hz" },
//        new Specification { Id=54, ProductId=13, SpecName="Chip", SpecValue="Dimensity 8200" },
//        new Specification { Id=55, ProductId=13, SpecName="Camera", SpecValue="50MP + 8MP + 32MP" },
//        new Specification { Id=56, ProductId=13, SpecName="Pin", SpecValue="4600 mAh, sạc 80W" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 14,
//    Name = "Realme 11 Pro+",
//    Brand = "Realme",
//    Price = 10990000,
//    Description = "Camera 200MP, thiết kế mặt lưng da độc đáo.",
//    Stock = 15,
//    CreatedAt = DateTime.Now.AddDays(-12),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=25, ProductId=14, ImageUrl="/images/realme11pro.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=57, ProductId=14, SpecName="Màn hình", SpecValue="6.7 inch AMOLED 120Hz" },
//        new Specification { Id=58, ProductId=14, SpecName="Chip", SpecValue="Dimensity 7050" },
//        new Specification { Id=59, ProductId=14, SpecName="Camera", SpecValue="200MP + 8MP + 2MP" },
//        new Specification { Id=60, ProductId=14, SpecName="Pin", SpecValue="5000 mAh, 100W" }
//    },

//    Reviews = new List<Reviews>()
//},

//new Product {
//    Id = 15,
//    Name = "Vivo V30",
//    Brand = "Vivo",
//    Price = 11990000,
//    Description = "Camera chụp chân dung đẹp, hiệu năng mượt, pin lớn.",
//    Stock = 17,
//    CreatedAt = DateTime.Now.AddDays(-4),

//    Images = new List<ProductImage>
//    {
//        new ProductImage { Id=26, ProductId=15, ImageUrl="/images/v30.jpg", IsMain=true }
//    },

//    Specifications = new List<Specification>
//    {
//        new Specification { Id=61, ProductId=15, SpecName="Màn hình", SpecValue="6.78 inch AMOLED 120Hz" },
//        new Specification { Id=62, ProductId=15, SpecName="Chip", SpecValue="Snapdragon 7 Gen 3" },
//        new Specification { Id=63, ProductId=15, SpecName="Camera", SpecValue="50MP + 50MP" },
//        new Specification { Id=64, ProductId=15, SpecName="Pin", SpecValue="5000 mAh, 80W" }
//    },

>>>>>>> main
//    Reviews = new List<Reviews>()
//},



//                };
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> 16b8ce6228f999353b3fe3e5712299214bb452a7
=======
>>>>>>> 16b8ce6228f999353b3fe3e5712299214bb452a7
=======
>>>>>>> 16b8ce6228f999353b3fe3e5712299214bb452a7
=======
>>>>>>> main

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var p = new Product()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Price = reader.GetDecimal("price"),
                            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                            Brand = reader.IsDBNull(reader.GetOrdinal("brand")) ? "" : reader.GetString("brand"),
                            Image = reader.IsDBNull(reader.GetOrdinal("main_image_url")) ? "" : reader.GetString("main_image_url"),
                            Images = new List<ProductImage>()
                        };
                        if (!string.IsNullOrEmpty(p.Image)) p.Images.Add(new ProductImage { ImageUrl = p.Image, IsMain = true });
                        list.Add(p);
                    }
                }
            }
            return list;
        }

        public static Product GetProductById(int id, string connectionString)
        {
            Product p = null;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM products WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        p = new Product()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Price = reader.GetDecimal("price"),
                            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                            Brand = reader.IsDBNull(reader.GetOrdinal("brand")) ? "" : reader.GetString("brand"),
                            Image = reader.IsDBNull(reader.GetOrdinal("main_image_url")) ? "" : reader.GetString("main_image_url"),
                            Images = new List<ProductImage>(),
                            Specifications = new List<Specification>(),
                            Reviews = new List<Reviews>()
                        };
                        if(!string.IsNullOrEmpty(p.Image)) p.Images.Add(new ProductImage { ImageUrl = p.Image, IsMain = true });
                    }
                }
            }
            return p;
        }
    }
}