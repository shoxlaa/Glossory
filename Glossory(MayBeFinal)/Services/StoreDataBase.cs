using Glossory_MayBeFinal_.Models;
using System;
using System.Collections.ObjectModel;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Glossory_MayBeFinal_.Services
{
    public class StoreDataBase : IStoreDataBase
    {
        private readonly SqlConnection _connection;

        public StoreDataBase()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Store"].ConnectionString);

            _connection.Open();
        }

        public Task AddProduct(Product product)
        {
            return Task.Run(() =>
            {
                using var command = new SqlCommand("INSERT INTO Products VALUES(@ProductName, @Coast, @ProductAmount, @Category, @Description)", _connection);

                command.Parameters.AddWithValue("ProductName", product.ProductName);
                command.Parameters.AddWithValue("Coast", product.Coast);
                command.Parameters.AddWithValue("Description", product.Description);
                command.Parameters.AddWithValue("ProductAmount", product.ProductAmount);
                command.Parameters.AddWithValue("Category", product.Category);

                command.ExecuteNonQuery();
            });
        }

        public Task DeleteProduct(Product product)
        {
            return Task.Run(() =>
            {
                var command = new SqlCommand("DELETE FROM [Products]  WHERE  ProductId=@ProductId", _connection);

                command.Parameters.AddWithValue("ProductId", product.ProductId);

                command.ExecuteNonQuery();
            });
        }

        public Task<ObservableCollection<Product>> GetProductByCategory(string category)
        {
            return Task.Run(() =>
             {
                 var products = new ObservableCollection<Product>();

                 var command = new SqlCommand("Select * FROM Products Where Category=@Category", _connection);

                 command.Parameters.AddWithValue("Category", category);

                 using var reader = command.ExecuteReader();
                 while (reader.Read())
                 {
                     var product = new Product
                     {
                         ProductId = Convert.ToInt32(reader["ProductId"]),
                         Category = reader["Category"].ToString(),
                         Description = reader["Description"].ToString(),
                         Coast = (float)Convert.ToDouble(reader["Coast"]),
                         ProductAmount = Convert.ToInt32(reader["ProductAmount"]),
                         ProductName = Convert.ToString(reader["ProductName"]),
                     };
                     products.Add(product);
                 }
                 return products;
             });
        }

        public Task<ObservableCollection<Product>> GetProductByName(string name)
        {
            return Task.Run(() =>
            {
                var products = new ObservableCollection<Product>();

                var command = new SqlCommand("Select * FROM Products Where ProductName=@ProductName", _connection);

                command.Parameters.AddWithValue("ProductName", name);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var product = new Product
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        Category = reader["Category"].ToString(),
                        Description = reader["Description"].ToString(),
                        Coast = (float)Convert.ToDouble(reader["Coast"]),
                        ProductAmount = Convert.ToInt32(reader["ProductAmount"]),
                        ProductName = Convert.ToString(reader["ProductName"]),
                    };
                    products.Add(product);
                }
                return products;
            });
        }

        public Task<ObservableCollection<Product>> SelectAll()
        {
            return Task.Run(() =>
            {
                var products = new ObservableCollection<Product>();

                var command = new SqlCommand("Select * FROM [Products]", _connection);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var product = new Product
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        Category = reader["Category"].ToString(),
                        Description = reader["Description"].ToString(),
                        Coast = (float)Convert.ToDouble(reader["Coast"]),
                        ProductAmount = Convert.ToInt32(reader["ProductAmount"]),
                        ProductName = Convert.ToString(reader["ProductName"]),
                    };
                    products.Add(product);
                }
                return products;
            });

        }
        public Task UpdateAmount(Product product, int productAmount)
        {
            return Task.Run(() =>
            {
                var command = new SqlCommand("UPDATE Products SET Amount=@new WHERE Amount=@prev", _connection);

                command.Parameters.AddWithValue("new", productAmount);
                command.Parameters.AddWithValue("prev", product.Coast);

                command.ExecuteNonQuery();
            });
        }

        public Task UpdateCategory(Product product, string category)
        {
            return Task.Run(() =>
            {
                var command = new SqlCommand("UPDATE Products SET Category=@new WHERE Category=@prev", _connection);

                command.Parameters.AddWithValue("new", category);
                command.Parameters.AddWithValue("prev", product.Category);

                command.ExecuteNonQuery();
            });
        }

        public Task UpdateCoast(Product product, float coast)
        {
            return Task.Run(() =>
            {
                var command = new SqlCommand("UPDATE Products SET Coast=@new WHERE Coast=@prev", _connection);

                command.Parameters.AddWithValue("new", coast);
                command.Parameters.AddWithValue("prev", product.Coast);

                command.ExecuteNonQuery();
            });
        }

        public Task UpdateDescription(Product product, string description)
        {
            return Task.Run(() =>
            {
                var command = new SqlCommand("UPDATE Products SET Description=@new WHERE Description=@prev", _connection);

                command.Parameters.AddWithValue("new", description);
                command.Parameters.AddWithValue("prev", product.Description);

                command.ExecuteNonQuery();
            });
        }

        public Task UpdateName(Product product, string productName)
        {
            return Task.Run(() =>
            {
                var command = new SqlCommand("UPDATE Products SET ProductName=@new WHERE ProductName=@prev", _connection);
                command.Parameters.AddWithValue("new", productName);
                command.Parameters.AddWithValue("prev", product.ProductName);

                command.ExecuteNonQuery();
            });
        }
    }
}
