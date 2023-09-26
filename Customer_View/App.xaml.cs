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
            
            MovementCallback movementCallback = new MovementCallback();
            InstanceContext context = new InstanceContext(movementCallback);    
            IMovementService movementService = new MovementServiceClient(context);
            ICustomerService customerService = new CustomerServiceClient();

            CustomerViewModel customerViewModel = CustomerViewModel.LoadCustomerViewModel(movementService, customerService);
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
            base.OnExit(e);
        }
    }
}
