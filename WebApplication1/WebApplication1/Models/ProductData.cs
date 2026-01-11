
using MySqlConnector;
using OpenXmlPowerTools;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ProductData
    {
        /* lấy p */
        public static List<Products> GetProducts(string connectionString, string search, string brand)
        {
            List<Products> list = new List<Products>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();


                string query = @"
                SELECT id, name, description, price, main_image_url, brand
                FROM products
                WHERE 1=1";

                if (!string.IsNullOrEmpty(search))
                    query += " AND name LIKE @search";

                if (!string.IsNullOrEmpty(brand))
                    query += " AND brand = @brand";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                if (!string.IsNullOrEmpty(search)) cmd.Parameters.AddWithValue("@search", "%" + search + "%");


                if (!string.IsNullOrEmpty(brand)) cmd.Parameters.AddWithValue("@brand", brand);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var p = new Products()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Price = reader.GetDecimal("price"),
                            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                            Brand = reader.IsDBNull(reader.GetOrdinal("brand")) ? "" : reader.GetString("brand"),
                            MainImageUrl = reader.IsDBNull(reader.GetOrdinal("main_image_url")) ? "" : reader.GetString("main_image_url"),
                            Images = new List<ProductImage>()
                        };

                        // Gán thêm các list sau khi tạo object
                        p.Specifications = GetSpecificationsByProductId(p.Id, connectionString);
                        p.Reviews = GetReviewsByProductId(p.Id, connectionString);
                        p.Images = GetProductImagesByProductId(p.Id, connectionString);

                        if (!string.IsNullOrEmpty(p.MainImageUrl))
                            p.Images.Add(new ProductImage { ImageUrl = p.MainImageUrl, IsMain = true });

                        list.Add(p);
                    }
                }
            }
            return list;
        }

        /* lấy productid */
        public static Products GetProductById(int id, string connectionString)
        {
            Products p = null;
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
                        p = new Products()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Price = reader.GetDecimal("price"),
                            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                            Brand = reader.IsDBNull(reader.GetOrdinal("brand")) ? "" : reader.GetString("brand"),
                            MainImageUrl = reader.IsDBNull(reader.GetOrdinal("main_image_url")) ? "" : reader.GetString("main_image_url"),
                            Images = new List<ProductImage>(),
                            Specifications = new List<Specification>(),
                            Reviews = new List<Reviews>()
                        };
                        if (!string.IsNullOrEmpty(p.MainImageUrl)) p.Images.Add(new ProductImage { ImageUrl = p.MainImageUrl, IsMain = true });
                    }
                }
            }
            return p;
        }

        /* lấy list ảnh */
        public static List<ProductImage> GetProductImagesByProductId(int productId, string connectionString)
        {
            var images = new List<ProductImage>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, image_url, is_main FROM product_images WHERE product_id = @pid";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pid", productId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        images.Add(new ProductImage
                        {
                            Id = reader.GetInt32("id"),
                            ImageUrl = reader.GetString("image_url"),
                            IsMain = reader.GetBoolean("is_main")
                        });
                    }
                }
            }
            return images;
        }

        /* lấy spec */
        public static List<Specification> GetSpecificationsByProductId(int productId, string connectionString)
        {
            var specs = new List<Specification>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, spec_name, spec_value FROM specifications WHERE product_id = @pid";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pid", productId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        specs.Add(new Specification
                        {
                            Id = reader.GetInt32("id"),
                            SpecName = reader.GetString("spec_name"),
                            SpecValue = reader.GetString("spec_value"),
                            ProductId = productId
                        });
                    }
                }
            }
            return specs;
        }

        /* lấy review */
        public static List<Reviews> GetReviewsByProductId(int productId, string connectionString)
        {
            var reviews = new List<Reviews>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, rating, review_comment, created_at, user_id, user_name FROM reviews WHERE product_id = @pid";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pid", productId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new Reviews
                        {
                            Id = reader.GetInt32("id"),
                            Rating = reader.GetInt32("rating"),
                            Comment = reader.IsDBNull(reader.GetOrdinal("review_comment")) ? "" : reader.GetString("review_comment"),
                            CreatedAt = reader.GetDateTime("created_at"),
                            UserId = reader.IsDBNull(reader.GetOrdinal("user_id")) ? null : reader.GetInt32("user_id"),
                            UserName = reader.IsDBNull(reader.GetOrdinal("user_name")) ? null : reader.GetString("user_name"),
                            ProductId = productId
                        });
                    }
                }
            }
            return reviews;
        }

        /* Lấy variant */
        public static ProductVariant GetVariantById(int variantId, string connectionString)
        {
            ProductVariant variant = null;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM product_variants WHERE id = @vid";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@vid", variantId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        variant = new ProductVariant
                        {
                            Id = reader.GetInt32("id"),
                            ProductId = reader.GetInt32("product_id"),
                            Color = reader.GetString("color"),
                            Memory = reader.GetString("memory"),
                            Price = reader.GetDecimal("price"),
                            Stock = reader.GetInt32("stock"),
                            ImageUrl = reader.IsDBNull(reader.GetOrdinal("image_url")) ? "" : reader.GetString("image_url")
                        };
                    }
                }
            }
            return variant;
        }
    }
}