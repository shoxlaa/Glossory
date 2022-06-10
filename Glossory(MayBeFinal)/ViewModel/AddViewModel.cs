using Glossory_MayBeFinal_.Models;
using Glossory_MayBeFinal_.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossory_MayBeFinal_.ViewModel
{
    [INotifyPropertyChanged]

    public partial class AddViewModel :BaseViewModel
    {
       
        private readonly IStoreDataBase _storeData;
        public AddViewModel(IStoreDataBase storeData)
        {
            _storeData = storeData;
        }
        [ICommand]
        private void Back()
        {
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<ViewModelType>(ViewModelType.StoreViewModel));
        }
        // ProductId  ProductName Coast ProductAmount Category Description

      
        [ObservableProperty]
        private string? _productName;
        [ObservableProperty]
        private string _coast;
        [ObservableProperty]
        private string _amount;
        [ObservableProperty]
        private string _category;
        [ObservableProperty]
        private string _description;

        [ICommand]
        private void Add()
        {

            
              if (Category == null || Coast == null || Description == null || ProductName == null ||Amount == null)
                {
                  
                    return;
                }
            

            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<Product>(new Product()
            {
               ProductName = ProductName, 
               Coast = (float)Convert.ToDouble(Coast), 
               Description = Description, Category = Category, 
                ProductAmount = Convert.ToInt32(Amount)
            }));;

            ProductName = default; 
            Category = default; 
            Coast = default;
            Description = default;
            Amount = default;



            Back();
        }

    }
}
