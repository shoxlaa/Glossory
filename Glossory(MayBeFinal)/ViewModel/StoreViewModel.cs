using Glossory_MayBeFinal_.Models;
using Glossory_MayBeFinal_.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace Glossory_MayBeFinal_.ViewModel
{
    [INotifyPropertyChanged]
    public partial class StoreViewModel : BaseViewModel
    {
        private readonly IStoreDataBase _storeData;
        string jsonString;

        public StoreViewModel(IStoreDataBase storeDataBase)
        {
            _storeData = storeDataBase;
            _storeData.CreateDataBase();
            _storeData.CreateTable();
            Products = _storeData.SelectAll();



            WeakReferenceMessenger.Default.Register<ValueChangedMessage<Product>>(this, (sender, message) =>
            {
                Products.Add(message.Value);
                storeDataBase.AddProduct(message.Value);
            });



        }

        [ObservableProperty]
        Product _selectedProduct;
        [ObservableProperty] 
        private ObservableCollection<Product> _products;






        public bool ProductCategoryIsChecked { get; set; }
        public bool ProductNameIsChecked { get; set; } 
        public string SearchedText { get; set; } 

        [ICommand]
        public void Search()
        {
            
            if (SearchedText == string.Empty || SearchedText == null)
            {
                Products =   _storeData.SelectAll();
                return;
            }
            Products = new ObservableCollection<Product>();

            if (ProductCategoryIsChecked)
            {

                Products = _storeData.GetProductByName(SearchedText);

         
            }
            else if (ProductNameIsChecked)
            {
                Products = _storeData.GetProductByCategory(SearchedText);

            }
        }

        [ICommand]
        public void OpenAddWindow()
        {
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<ViewModelType>(ViewModelType.AddViewModel));
        }
        
        [ICommand] 
        public void Remove()
        {
            if(_selectedProduct != null)
            {
                _storeData.DeleteProductByName(SelectedProduct.ProductName);
                _products.Remove(SelectedProduct);
            }

        }

        [ICommand] 
        public void Update()
        {

            foreach (var item in Products)
            {
                if (item.Category == null  || item.ProductId == null || item.Coast == null||  item.Description == null  ||item.ProductName == null || item.ProductAmount == null )
                {
                    Products = _storeData.SelectAll();
                    MessageBox.Show("Something go wrong!");
                    return;
                }
            }

            JsonUpdate();

            Products = JsonDes(); 
            _storeData.DropTable(); 

           _storeData.CreateTable(); 
            foreach(var product in Products)
            {
                _storeData.AddProduct(product);
            }


            Products = _storeData.SelectAll();



        }

        [ICommand] 
        public void JsonUpdate()
        {



            MessageBox.Show("json was updated");

            string fileName = "ProductsCollection.json";
            jsonString = JsonSerializer.Serialize(_products); 

            File.WriteAllText(fileName, jsonString);
        } 

        public ObservableCollection<Product> JsonDes()
        {
            string fileName = "ProductsCollection.json";
            string jsonString = File.ReadAllText(fileName);
          return  JsonSerializer.Deserialize<ObservableCollection<Product>>(jsonString)!;
        }



       


    }

}
