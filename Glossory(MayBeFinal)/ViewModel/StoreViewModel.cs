using Glossory_MayBeFinal_.Models;
using Glossory_MayBeFinal_.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace Glossory_MayBeFinal_.ViewModel
{
    public partial class StoreViewModel : BaseViewModel
    {
        private readonly IStoreDataBase _storeData;

        public StoreViewModel(IStoreDataBase storeDataBase)
        {
            _storeData = storeDataBase;
            Products = _storeData.SelectAll();

            WeakReferenceMessenger.Default.Register<ValueChangedMessage<Product>>(this, (sender, message) =>
            {
                storeDataBase.AddProduct(message.Value);
                Products = _storeData.SelectAll();
            });
        }

        [ObservableProperty]
        Product? _selectedProduct;

        [ObservableProperty]
        private ObservableCollection<Product>? _products;

        public bool ProductNameIsChecked { get; set; } = true;
        public bool ProductCategoryIsChecked { get; set; }
        public string? SearchedText { get; set; }

        [ICommand]
        private void Search()
        {
            if (string.IsNullOrEmpty(SearchedText))
            {
                Products = _storeData.SelectAll();
            }
            else if (ProductCategoryIsChecked)
            {
                Products = _storeData.GetProductByName(SearchedText);
            }
            else if (ProductNameIsChecked)
            {
                Products = _storeData.GetProductByCategory(SearchedText);
            }
        }

        [ICommand]
        private void OpenAddWindow()
        {
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<ViewModelType>(ViewModelType.AddViewModel));
        }

        [ICommand]
        private void Remove()
        {
            if (SelectedProduct != null)
            {
                _storeData.DeleteProduct(SelectedProduct);
                Products = _storeData.SelectAll();
            }
        }

        [ICommand]
        private void Update()
        {
            var OldProducts = _storeData.SelectAll();

            foreach (var item in Products!)
            {
                if (string.IsNullOrWhiteSpace(item.ProductName) || string.IsNullOrWhiteSpace(item.Category) || string.IsNullOrWhiteSpace(item.Description) || item.Coast <= 0 || item.ProductAmount <= 0)
                {
                    MessageBox.Show("Field is empty!");
                    return;
                }
            }

            for (int i = 0; i < OldProducts.Count; i++)
            {
                if (OldProducts[i].ProductName != Products[i].ProductName)
                {
                    _storeData.UpdateName(OldProducts[i], Products[i].ProductName);
                }
                if (OldProducts[i].Category != Products[i].Category)
                {
                    _storeData.UpdateCategory(OldProducts[i], Products[i].Category);
                }
                if (OldProducts[i].Description != Products[i].Description)
                {
                    _storeData.UpdateDescription(OldProducts[i], Products[i].Description);
                }
                if (OldProducts[i].Coast != Products[i].Coast)
                {
                    _storeData.UpdateCoast(OldProducts[i], Products[i].Coast);
                }
                if (OldProducts[i].ProductAmount != Products[i].ProductAmount)
                {
                    _storeData.UpdateAmount(OldProducts[i], Products[i].ProductAmount);
                }
            }
        }

        [ICommand]
        private void Restore()
        {
            Products = _storeData.SelectAll();
        }
    }
}
