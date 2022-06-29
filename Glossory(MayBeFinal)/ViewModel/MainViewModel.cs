using Microsoft.Toolkit.Mvvm.ComponentModel;
using Glossory_MayBeFinal_.Views;
using Glossory_MayBeFinal_.Services;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;

namespace Glossory_MayBeFinal_.ViewModel
{
	public partial class MainViewModel : BaseViewModel
    {
		private readonly ViewModelFactory _factory;

		[ObservableProperty]
		private BaseViewModel? _currentViewModel;

		public MainViewModel(ViewModelFactory factory)
		{
			_factory = factory;

			CurrentViewModel = (BaseViewModel?)factory.Create(ViewModelType.StoreViewModel);

			WeakReferenceMessenger.Default.Register<ValueChangedMessage<ViewModelType>>(this, (sender, message) =>
			{
				CurrentViewModel = (BaseViewModel?)_factory.Create(message.Value);
			});
		}
	}


}
