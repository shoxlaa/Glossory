using Glossory_MayBeFinal_.Models;
using Glossory_MayBeFinal_.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Glossory_MayBeFinal_.ViewModel
{
    public partial class StoreViewModel : BaseViewModel
    {
        private readonly IStoreDataBase _storeData;
        [ObservableProperty]
        Product? _selectedProduct;
        [ObservableProperty]
        private ObservableCollection<Product>? _products;
        public bool ProductNameIsChecked { get; set; } = true;
        public bool ProductCategoryIsChecked { get; set; }
        public string? SearchedText { get; set; }
        public StoreViewModel(IStoreDataBase storeDataBase)
        {
            _storeData = storeDataBase;

            Restore();

            WeakReferenceMessenger.Default.Register<ValueChangedMessage<Product>>(this, AddWindow);

        }
        private async void AddWindow(object recipient, ValueChangedMessage<Product> message)
        {
            await _storeData.AddProduct(message.Value);
        }
        [ICommand]
        private void OpenAddWindow()
        {
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<ViewModelType>(ViewModelType.AddViewModel));
        }
        [ICommand]
        private async void Search()
        {
            if (string.IsNullOrEmpty(SearchedText))
            {
                Restore();
            }
            else if (ProductCategoryIsChecked)
            {
                Products = await _storeData.GetProductByCategory(SearchedText);
            }
            else if (ProductNameIsChecked)
            {
                Products = await _storeData.GetProductByName(SearchedText);
            }
        }
        [ICommand]
        private async void Remove()
        {
            if (SelectedProduct != null)
            {
                await _storeData.DeleteProduct(SelectedProduct);
            }
        }
        [ICommand]
        private async void Update()
        {
            var OldProducts = await _storeData.SelectAll();

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
                    await _storeData.UpdateName(OldProducts[i], Products[i].ProductName);
                }
                if (OldProducts[i].Category != Products[i].Category)
                {
                    await _storeData.UpdateCategory(OldProducts[i], Products[i].Category);
                }
                if (OldProducts[i].Description != Products[i].Description)
                {
                    await _storeData.UpdateDescription(OldProducts[i], Products[i].Description);
                }
                if (OldProducts[i].Coast != Products[i].Coast)
                {
                    await _storeData.UpdateCoast(OldProducts[i], Products[i].Coast);
                }
                if (OldProducts[i].ProductAmount != Products[i].ProductAmount)
                {
                    await _storeData.UpdateAmount(OldProducts[i], Products[i].ProductAmount);
                }
            }
        }
        [ICommand]
        private async void Restore()
        {
            Products = await _storeData.SelectAll();
        }
    }
}
