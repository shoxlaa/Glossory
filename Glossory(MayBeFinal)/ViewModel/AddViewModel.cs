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
    public partial class AddViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string? _productName;
        [ObservableProperty]
        private float _coast;
        [ObservableProperty]
        private int _amount;
        [ObservableProperty]
        private string? _category;
        [ObservableProperty]
        private string? _description;
        [ICommand]
        private void Back()
        {
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<ViewModelType>(ViewModelType.StoreViewModel));
        }
        [ICommand]
        private void Add()
        {
            if (string.IsNullOrWhiteSpace(Category) || Coast <= 0 || string.IsNullOrWhiteSpace(Description) || string.IsNullOrWhiteSpace(ProductName) || Amount <= 0)
            {
                return;
            }
            Back();
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<Product>(new Product()
            {
                ProductName = ProductName,
                Coast = Coast,
                Description = Description,
                Category = Category,
                ProductAmount = Amount
            }));

            ProductName = null;
            Category = null;
            Description = null;
            Coast = default;
            Amount = default;
        }
    }
}
