using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using ViewModels;
using ViewModels.Adapters;
using ViewModels.Base;
using ViewModels.MovementService;
using ViewModels.PaymentBoxService;
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

        /// <summary>
        /// OnStartup method to load the MainWindow and set the DataContext to the MainViewModel
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            InstanceContext context = new InstanceContext(new MovementCallback());
            IMovementService movementService = new MovementServiceClient(context);
            IPaymentBoxService paymentBoxService = new PaymentBoxServiceClient();
            ServiceParameter loginViewParameter = new ServiceParameter(movementService, paymentBoxService);

            navigationStore.CurrentViewModel = CreateLoginViewModel(loginViewParameter);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        /// <summary>
        /// CreateLoginViewModel method to create a LoginViewModel
        /// </summary>
        /// <returns>a LoginViewModel</returns>
        private LoginViewModel CreateLoginViewModel(ServiceParameter loginViewParameter)
        {
            var navigationService = new ParameterNavigationService<PaymentBoxViewParameter>(navigationStore, CreatePaymentBoxViewModel);
            return LoginViewModel.LoadLoginViewModel(navigationService, loginViewParameter);
        }

        /// <summary>
        /// CreatePaymentBoxViewModel method to create a PaymentBoxViewModel
        /// </summary>
        /// <param name="paymentBox"> PaymentBoxViewParameter to pass to the PaymentBoxViewModel</param>
        /// <returns></returns>
        private PaymentBoxViewModel CreatePaymentBoxViewModel(PaymentBoxViewParameter paymentBoxViewParameter)
        {
            return new PaymentBoxViewModel(new ParameterNavigationService<ServiceParameter>(navigationStore, CreateLoginViewModel), paymentBoxViewParameter);
        }
    }
}
