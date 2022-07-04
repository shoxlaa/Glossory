using Glossory_MayBeFinal_.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Glossory_MayBeFinal_.Services
{
    public interface IStoreDataBase
    {
        Task<ObservableCollection<Product>> SelectAll();
        Task AddProduct(Product product);
        Task DeleteProduct(Product product);
        Task< ObservableCollection<Product>> GetProductByName(string name);
        Task<ObservableCollection<Product>> GetProductByCategory(string category);
        Task UpdateName(Product product, string productName);
        Task UpdateCategory(Product product, string category);
        Task UpdateDescription(Product product, string description);
        Task UpdateCoast(Product product, float coast);
        Task UpdateAmount(Product product, int productAmount);
    }

}
