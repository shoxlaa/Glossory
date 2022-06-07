using Glossory_MayBeFinal_.Models;
using Glossory_MayBeFinal_.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.Json;

namespace Glossory_MayBeFinal_.ViewModel
{
    [INotifyPropertyChanged]
    public partial class StoreViewModel : BaseViewModel
    {
        private readonly IStoreDataBase _storeData;
        [ObservableProperty]
        Product _selectedProduct;
        [ObservableProperty] 
        private ObservableCollection<Product> _products;
        string jsonString;


       [ICommand]
        public void OpenAddWindow()
        {
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<ViewModelType>(ViewModelType.AddViewModel));
        }
        [ICommand]
        public void OpenDiscription()
        {
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<ViewModelType>(ViewModelType.DescriptionViewModel));

        }
        [ICommand] 
        public void Remove()
        {
            _storeData.DeleteProductByName(SelectedProduct.ProductName); 
            _products.Remove(SelectedProduct); 

        }

        [ICommand] 
        public void Update()
        {
            Products = _storeData.SelectAll();

        }

        [ICommand] 
        public void JsonUpdate()
        {
           jsonString = JsonSerializer.Serialize(_products);
        }

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


    }

}
