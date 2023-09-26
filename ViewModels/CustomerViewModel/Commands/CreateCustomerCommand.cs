using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModels.Base;
using ViewModels.CustomerService;
using ViewModels.MovementService;

namespace ViewModels.CustomerViewModel.Commands
{
    /// <summary>
    /// CreateCustomerCommand class. This class represents the command for creating a Customer.
    /// </summary>
    public class CreateCustomerCommand : AsyncCommandBase
    {
        private readonly CustomerViewModel customerViewModel;
        private readonly ICustomerService customerService;
        public CreateCustomerCommand(CustomerViewModel customerViewModel, ICustomerService customerService)
        {
            this.customerService = customerService;
            this.customerViewModel = customerViewModel;
            customerViewModel.PropertyChanged += CustomerViewModel_PropertyChanged;
        }


        private void CustomerViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CustomerViewModel.CustomerName):
                    OnCanExecuteChanged();
                    break;
                default:
                    break;
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(customerViewModel.CustomerName) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Customer customer = new Customer()
            {
                Name = customerViewModel.CustomerName,
                LastName = customerViewModel.CustomerLastName,
                RegistrationDate = DateTime.Now
            };

            try
            {
                customer.Id = await customerService.AddAsync(customer);
                customerViewModel.Customers.Enqueue(new CustomerModelAdapter(customer));
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to create Customer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
