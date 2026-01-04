using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace WebApplication1.Models
{
    public class ProductData
    {
        
        public static List<Product> GetProducts(string connectionString, string search, string brand)
        {
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