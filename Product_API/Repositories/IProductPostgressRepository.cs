using Npgsql;
using Product_API.Models;

namespace Product_API.Repositories
{
    public class ProductPostgressRepository : IProductRepository
    {
        const string connectionstring = "Server=127.0.0.1;Port=5432;Database=MongoDb;username=postgres;Password=xushnud;";


        public Product Add(Product product)
        {
            using(var connection = new  NpgsqlConnection(connectionstring))
            {
                connection.Open();
                string query = $"Insert into products(Name,Description,PhotoPath) values({product.Name}, {product.Description}, {product.PhotoPath})";
                NpgsqlCommand npgsqlBatchCommand = new NpgsqlCommand(query,connection);
                npgsqlBatchCommand.ExecuteNonQuery();
            }
            return product;
        }

        public List<Product> GetAll()
        {
            using(var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                List<Product> products = new List<Product>();
                Product product = new Product();

                string query = $@"Select * from products";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    product.Name = reader.GetString(0);
                    product.Description = reader.GetString(1);
                    product.PhotoPath = reader.GetString(2);
                    products.Add(product);
                }

                return products;



            }
            
        }
    }
}
