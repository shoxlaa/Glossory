using Glossory_MayBeFinal_.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossory_MayBeFinal_.Services
{
    public class StoreDataBaseService :IStoreDataBase
    {
        private SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString);

        public void CreateDataBase()
        {
            Connection.Open();
            var command = new SqlCommand()
            {
                CommandText = "USE master   IF NOT EXISTS(        SELECT[name]           " +
                "     FROM sys.databases" +
                "                WHERE[name] = N'StoreDb') CREATE DATABASE Store   ",
                CommandType = CommandType.Text,
                Connection = Connection
            };


            command.ExecuteNonQuery();
            Connection.Close();

        }
        public void CreateTable()
        {

            Connection.Open();
            try
            {

                var command = new SqlCommand()
                {
                    CommandText = @"Use [Store];  
IF OBJECT_ID('[dbo].[Products]', 'U') IS NULL  CREATE TABLE [Products]
(
        
        [ProductId] INT PRIMARY KEY IDENTITY(1, 1),
        [ProductName] NVARCHAR(MAX),
        [Coast]  FLoat,
        [ProductAmount] INT,
[Category] NVARCHAR(MAX)
,[Description] NVARCHAR(MAX)
        
)
",
                    CommandType = CommandType.Text,
                    Connection = Connection

                };

                command.ExecuteNonQuery();
                Connection.Close();

            }
            catch (Exception ex)
            {
                return;
            } 
        }
        public void AddProduct(Product product)
        {
            try
            {

                Connection.Open();
                using var command = new SqlCommand()
                {
                    CommandText = "USE [Store];INSERT INTO [Products] VALUES(@ProductName" +
                    ",@Coast , @ProductAmount,@Category, @Description)",
                    CommandType = CommandType.Text,
                    Connection = Connection
                };

                command.Parameters.AddWithValue("ProductName", product.ProductName);
                command.Parameters.AddWithValue("Coast", product.Coast);
                command.Parameters.AddWithValue("ProductAmount", product.ProductAmount);
                command.Parameters.AddWithValue("Category", product.Category);
                command.Parameters.AddWithValue("Description", product.Description);

                command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)

            {
                return;
            }
            Connection.Close();

        }
        public void DeleteProductByName(string _productName)
        {
            try
            {
                Connection.Open();

                var command = new SqlCommand()
                {
                    CommandText = " USE [Store]; DELETE FROM [Products]  WHERE ProductName=@ProductName",
                    CommandType = CommandType.Text,
                    Connection = Connection
                };

                command.Parameters.AddWithValue("ProductName", _productName);
                command.ExecuteNonQuery();
                Connection.Close();

            }
            catch (Exception ex) { }

        }
        public void DeleteProductByCategory(string _categoryName)
        {
            try
            {
                Connection.Open();

                var command = new SqlCommand()
                {
                    CommandText = "USE [Store]; DELETE FROM [Products]  WHERE Category=@CategoryName",
                    CommandType = CommandType.Text,
                    Connection = Connection
                };

                command.Parameters.AddWithValue("@CategoryName", _categoryName);
                command.ExecuteNonQuery();

                Connection.Close();

            }
            catch (Exception ex)
            {
            }
        }
        public void UpdateCoast(Product product, float _newCoast)
        {
            try
            {


                var command = new SqlCommand()
                {
                    CommandText = "USE [Store]; " +
                    "UPDATE Products SET Coast=@newCoast WHERE Coast=@prevCoast",
                    CommandType = CommandType.Text,
                    Connection = Connection
                };
                command.Parameters.AddWithValue("newCoast", _newCoast);
                command.Parameters.AddWithValue("prevCoast", product.Coast);

                product.Coast = _newCoast;
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();


            }
            catch (Exception ex)
            {
            }
        }
        public void UpdateCategory(Product product, string _newCategory)
        {
            try
            {


                var command = new SqlCommand()
                {
                    CommandText = "USE [Store]; " +
                    "UPDATE Products SET Category=@newCategory WHERE Category=@prevCategory",
                    CommandType = CommandType.Text,
                    Connection = Connection
                };
                command.Parameters.AddWithValue("newCategory", _newCategory);
                command.Parameters.AddWithValue("prevCategory", product.Category);

                product.Category = _newCategory;
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();


            }
            catch (Exception ex)
            {
            }
        }
        public void UpdateAmount(Product product, int _newAmount)
        {
            try
            {


                var command = new SqlCommand()
                {
                    CommandText = "USE [Store]; " +
                    "UPDATE Products SET Category=@newAmount WHERE Category=@prevAmount",
                    CommandType = CommandType.Text,
                    Connection = Connection
                };
                command.Parameters.AddWithValue("newAmount", _newAmount);
                command.Parameters.AddWithValue("prevAmount", product.ProductAmount);

                product.ProductAmount = _newAmount;
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();


            }
            catch (Exception ex)
            {
            }

        }
        public void UpdateName(Product product, string _newName)
        {
            try
            {


                var command = new SqlCommand()
                {
                    CommandText = "USE [Store]; " +
                    "UPDATE Products SET Category=@newName WHERE Category=@prevName",
                    CommandType = CommandType.Text,
                    Connection = Connection
                };
                command.Parameters.AddWithValue("newName", _newName);
                command.Parameters.AddWithValue("prevName", product.ProductName);

                product.ProductName = _newName;
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();


            }
            catch (Exception ex)
            {
            }


        }
        public void UpdateDescription(Product product, string _newDescription)
        {
            try
            {


                var command = new SqlCommand()
                {
                    CommandText = "USE [Store]; " +
                    "UPDATE Products SET Category=@newDescription WHERE Category=@prevDescription",
                    CommandType = CommandType.Text,
                    Connection = Connection
                };
                command.Parameters.AddWithValue("newName", _newDescription);
                command.Parameters.AddWithValue("prevName", product.Description);

                product.Description = _newDescription;
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();


            }
            catch (Exception ex)
            {
            }

        }
        public Product GetProductById(int id)
        {
            Connection.Open();
            using var command = new SqlCommand()
            {
                CommandText = "USE [Store]; SELECT * FROM [Products] WHERE [ProductId] = @id",
                CommandType = CommandType.Text,
                Connection = Connection
            };

            command.Parameters.AddWithValue("id", id);

            using SqlDataReader reader = command.ExecuteReader();

            Product product = new Product();

            while (reader.Read())
            {
                //ProductName ",@Coast , @ProductAmount,@Category, @Description)",
                int productId = Convert.ToInt32(reader["ProductId"]);
                int productAmount = Convert.ToInt32(reader["ProductAmount"]);
                string productName = reader["ProductName"].ToString()!;
                string _ProductCoast = Convert.ToString(reader["Coast"]);
                float ProductCoast = (float)Convert.ToDouble(_ProductCoast.Replace(',', '.'));
                string Description = reader["Description"].ToString()!;

                product.ProductId = productId;
                product.ProductAmount = productAmount;
                product.ProductName = productName;
                product.Coast = ProductCoast;
                product.Description = Description;

            }

            reader.Close();

            Connection.Close();
            return product;
        }
        public Product GetProductByName(string _name)
        {
            Connection.Open();
            using var command = new SqlCommand()
            {
                CommandText = "USE [Store]; SELECT * FROM [Products] WHERE [ProductName] = @Name",
                CommandType = CommandType.Text,
                Connection = Connection
            };

            command.Parameters.AddWithValue("Name", _name);

            using SqlDataReader reader = command.ExecuteReader();

            Product product = new Product();

            while (reader.Read())
            {
                //ProductName ",@Coast , @ProductAmount,@Category, @Description)",
                int productId = Convert.ToInt32(reader["ProductId"]);
                int productAmount = Convert.ToInt32(reader["ProductAmount"]);
                string productName = reader["ProductName"].ToString()!;
                string _ProductCoast = Convert.ToString(reader["Coast"]);
                float ProductCoast = (float)Convert.ToDouble(_ProductCoast.Replace(',', '.'));
                string Description = reader["Description"].ToString()!;

                product.ProductId = productId;
                product.ProductAmount = productAmount;
                product.ProductName = productName;
                product.Coast = ProductCoast;
                product.Description = Description;

            }
            reader.Close();

            Connection.Close();
            return product;
        }

        public bool IsExistsProducts()
        {
            Connection!.Open();
            using var command = new SqlCommand()
            {
                CommandText = "USE Store; IF EXISTS (SELECT 1 FROM Products) SELECT 1  AS B  ELSE SELECT 0 AS B",
                CommandType = CommandType.Text,
                Connection = (SqlConnection)Connection!
            };

            using SqlDataReader reader = command.ExecuteReader();

            bool flag = false;
            while (reader.Read())
            {
                flag = Convert.ToBoolean(reader["B"]);
            }
            Connection.Close();

            return flag;
        }


        public ObservableCollection<Product> SelectAll()
        {
            if (IsExistsProducts() == true)
            {


                Connection.Open();
                using var command = new SqlCommand()
                {
                    CommandText = "USE [Store];  Select* from Products",
                    CommandType = CommandType.Text,
                    Connection = Connection
                };

                ObservableCollection<Product> products = new ObservableCollection<Product>();
                using SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Product product = new Product();

                    //ProductName ",@Coast , @ProductAmount,@Category, @Description)",
                    int productId = Convert.ToInt32(reader["ProductId"]);
                    int productAmount = Convert.ToInt32(reader["ProductAmount"]);
                    string productName = reader["ProductName"].ToString()!;
                    string _ProductCoast = Convert.ToString(reader["Coast"]);
                    float ProductCoast = (float)Convert.ToDouble(_ProductCoast);
                    string Description = reader["Description"].ToString()!; 
                    string Category = reader["Category"].ToString()!; 

                    product.ProductId = productId;
                    product.ProductAmount = productAmount;
                    product.ProductName = productName;
                    product.Coast = ProductCoast;
                    product.Description = Description; 
                    product.Category = Category;
                    products.Add(product);

                }
                Connection.Close();

                return products;
            } 
            else
            {
                return new ObservableCollection<Product>();  
            }
        }

    
    } 

}
