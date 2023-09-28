using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using ViewModels;
using ViewModels.Base;
using ViewModels.CustomerService;
using ViewModels.CustomerViewModel;
using ViewModels.MovementService;
using ViewModels.Services;
using ViewModels.Stores;

namespace Customer_View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IMovementService movementService;
        ICustomerService customerService;
        ICustomerViewModelService customerViewModelService;
        MovementCallback movementCallback;
        public App()
        {
            movementCallback = new MovementCallback();
            InstanceContext context = new InstanceContext(movementCallback);
            movementService = new MovementServiceClient(context);
            customerService = new CustomerServiceClient();
            customerViewModelService = new CustomerViewModelService(movementService, customerService);
        }

        /// <summary>
        /// OnStartup method to load the MainWindow and set the DataContext to the CustomerViewModel
        /// </summary>
        protected async override void OnStartup(StartupEventArgs e)
        {
            movementService.Subscribe();
            CustomerViewModel customerViewModel = await CustomerViewModel.LoadCustomerViewModel(customerViewModelService);
            movementCallback.DataRecived += customerViewModel.UpdateMovements;
            MainWindow = new MainWindow()
            {
                DataContext = customerViewModel
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        override protected void OnExit(ExitEventArgs e)
        {
            movementService.Unsubscribe();
            base.OnExit(e);
        }
    }
}
