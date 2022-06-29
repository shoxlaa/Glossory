using Glossory_MayBeFinal_.Models;
using System;
using System.Collections.ObjectModel;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Glossory_MayBeFinal_.Services
{
    public class StoreDataBaseMongoDb : IStoreDataBase
    {
        private readonly MongoClient _client = new();

        private readonly IMongoDatabase _dataBase;
        private readonly IMongoCollection<BsonDocument> _products;

        public StoreDataBaseMongoDb()
        {
            _dataBase = _client.GetDatabase("StoreDb");
            _products = _dataBase.GetCollection<BsonDocument>("products");
        }

        public void AddProduct(Product product)
        {
            _products.InsertOne(new BsonDocument
            {
                {"product_name", product.ProductName },
                {"product_amount", product.ProductAmount },
                {"coast", product.Coast },
                {"description", product.Description },
                {"category",product.Category }
            });
        }

        public void DeleteProduct(Product product)
        {
            _products.DeleteOne(new BsonDocument
            {
                {"product_name", product.ProductName },
                {"product_amount", product.ProductAmount },
                {"coast", product.Coast },
                {"description", product.Description },
                {"category",product.Category }
            });
        }

        public ObservableCollection<Product> GetProductByCategory(string category)
        {
            var products = _dataBase.GetCollection<Product>("products").Find("{}").ToList();

            return new ObservableCollection<Product>(products.Where(x => x.Category == category));
        }

        public ObservableCollection<Product> GetProductByName(string name)
        {
            var products = _dataBase.GetCollection<Product>("products").Find("{}").ToList();

            return new ObservableCollection<Product>(products.Where(x => x.ProductName == name));
        }

        public ObservableCollection<Product> SelectAll()
        {
            return new ObservableCollection<Product>(_dataBase.GetCollection<Product>("products").Find("{}").ToList());
        }

        public void UpdateAmount(Product product, int productAmount)
        {
            var filter = Builders<Product>.Filter.Where(x => x.ProductId == product.ProductId);
            _dataBase.GetCollection<Product>("products").UpdateOne(filter, Builders<Product>.Update.Set(x => x.ProductAmount, productAmount));
        }

        public void UpdateCategory(Product product, string category)
        {
            var filter = Builders<Product>.Filter.Where(x => x.ProductId == product.ProductId);
            _dataBase.GetCollection<Product>("products").UpdateOne(filter, Builders<Product>.Update.Set(x => x.Category, category));
        }

        public void UpdateCoast(Product product, float coast)
        {
            var filter = Builders<Product>.Filter.Where(x => x.ProductId == product.ProductId);
            _dataBase.GetCollection<Product>("products").UpdateOne(filter, Builders<Product>.Update.Set(x => x.Coast, coast));
        }

        public void UpdateDescription(Product product, string description)
        {
            var filter = Builders<Product>.Filter.Where(x => x.ProductId == product.ProductId);
            _dataBase.GetCollection<Product>("products").UpdateOne(filter, Builders<Product>.Update.Set(x => x.Description, description));
        }

        public void UpdateName(Product product, string productName)
        {
            var filter = Builders<Product>.Filter.Where(x => x.ProductId == product.ProductId);
            _dataBase.GetCollection<Product>("products").UpdateOne(filter, Builders<Product>.Update.Set(x => x.ProductName, productName));
        }
    }
}
