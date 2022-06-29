using System;
using System.Collections.ObjectModel;
using Glossory_MayBeFinal_.Models;

namespace Glossory_MayBeFinal_.Services;

public class StoreDbEntite : IStoreDataBase
{
    private StoreDbContext _context = new StoreDbContext();  
        
        

        
    public ObservableCollection<Product> SelectAll()
    {
        return _context.products.Local;  
    }

    public void CreateDataBase()
    {
        throw new NotImplementedException();
    }

    public void CreateTable()
    {
        _context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Products]");
          
    }

    public void AddProduct(Product value)
    {
        throw new NotImplementedException();
    }

    public void DeleteProductByName(string _productName)
    {
        throw new NotImplementedException();
    }

    public void DeleteProductByCategory(string _productName)
    {
        throw new NotImplementedException();
    }

    public void UpdateCoast(Product product, float _newCoast)
    {
        throw new NotImplementedException();
    }

    public void UpdateCategory(Product product, string _newCategory)
    {
        throw new NotImplementedException();
    }

    public void UpdateAmount(Product product, int _newAmount)
    {
        throw new NotImplementedException();
    }

    public void UpdateName(Product product, string _newName)
    {
        throw new NotImplementedException();
    }

    public void UpdateDescription(Product product, string _newDescription)
    {
        throw new NotImplementedException();
    }

    public ObservableCollection<Product> GetProductByName(string _name)
    {
        throw new NotImplementedException();
    }

    public ObservableCollection<Product> GetProductByCategory(string _category)
    {
        throw new NotImplementedException();
    }

    public void DropTable()
    {
        throw new NotImplementedException();
    }

    public bool IsExistsProducts()
    {
        throw new NotImplementedException();
    }
}