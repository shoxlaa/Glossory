using Glossory_MayBeFinal_.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Glossory_MayBeFinal_.Services
{
    public interface IStoreDataBase
    {
        ObservableCollection<Product> SelectAll();
        void AddProduct(Product product);
        void DeleteProduct(Product product);
        ObservableCollection<Product> GetProductByName(string name);
        ObservableCollection<Product> GetProductByCategory(string category);
        void UpdateName(Product product, string productName);
        void UpdateCategory(Product product, string category);
        void UpdateDescription(Product product, string description);
        void UpdateCoast(Product product, float coast);
        void UpdateAmount(Product product, int productAmount);
    }

}
