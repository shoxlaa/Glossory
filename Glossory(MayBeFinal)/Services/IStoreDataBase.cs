using Glossory_MayBeFinal_.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Glossory_MayBeFinal_.Services
{
    public interface IStoreDataBase
    {
        ObservableCollection <Product> SelectAll();
        void CreateDataBase();
        void CreateTable();
        void AddProduct(Product value);
        public void DeleteProductByName(string _productName); 
        public void DeleteProductByCategory(string _productName);
        public void UpdateCoast(Product product, float _newCoast);
        public void UpdateCategory(Product product, string _newCategory);
        public void UpdateAmount(Product product, int _newAmount);
        public void UpdateName(Product product, string _newName);
        public void UpdateDescription(Product product, string _newDescription);
        public Product GetProductById(int id);
        public Product GetProductByName(string _name);
        public bool IsExistsProducts();






    }

}
