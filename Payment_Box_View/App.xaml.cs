using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModels;
using ViewModels.Adapters;
using ViewModels.Services;
using ViewModels.Stores;

namespace Payment_Box_View
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
            navigationStore.CurrentViewModel = CreateLoginViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        private LoginViewModel CreateLoginViewModel()
        {
            return LoginViewModel.LoadLoginViewModel(new ParameterNavigationService<PaymentBoxViewParameter>(navigationStore, CreatePaymentBoxViewModel));
        }

        private PaymentBoxViewModel CreatePaymentBoxViewModel(PaymentBoxViewParameter paymentBox)
        {
            return new PaymentBoxViewModel(new NavigationService(navigationStore, CreateLoginViewModel), paymentBox);
        }
    }
}
