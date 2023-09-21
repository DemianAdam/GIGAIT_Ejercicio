using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModels;
using ViewModels.CustomerViewModel;
using ViewModels.Stores;

namespace Customer_View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore navigationStore;
        public App()
        {
            navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            //navigationStore.CurrentViewModel = new CustomerViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = CustomerViewModel.LoadCustomerViewModel()
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
