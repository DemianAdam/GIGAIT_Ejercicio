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

        /// <summary>
        /// OnStartup method to load the MainWindow and set the DataContext to the CustomerViewModel
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {

            MainWindow = new MainWindow()
            {
                DataContext = CustomerViewModel.LoadCustomerViewModel()
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
