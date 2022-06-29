using System;
using SimpleInjector;
using System.Windows;
using Glossory_MayBeFinal_.ViewModel;
using Glossory_MayBeFinal_.Services;
using Glossory_MayBeFinal_.Views;

namespace Glossory_MayBeFinal_
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container p_container { get; } = new();

        protected override void OnStartup(StartupEventArgs e)
        {
            p_container.RegisterSingleton<MainViewModel>();
            p_container.RegisterSingleton<StoreViewModel>();
            p_container.RegisterSingleton<AddViewModel>();
            p_container.RegisterSingleton<IStoreDataBase, StoreDataBaseMongoDb>();
            p_container.RegisterSingleton<ViewModelFactory>();
            p_container.RegisterSingleton<MainWindow>();

            var window = p_container.GetInstance<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }
    }
}
