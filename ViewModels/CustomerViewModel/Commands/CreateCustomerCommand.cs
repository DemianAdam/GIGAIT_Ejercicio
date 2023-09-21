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
    public class CreateCustomerCommand : AsyncCommandBase
    {
        private readonly CustomerViewModel customerViewModel;
        public CreateCustomerCommand(CustomerViewModel customerViewModel)
        {
            this.customerViewModel = customerViewModel;
            customerViewModel.PropertyChanged += CustomerViewModel_PropertyChanged;
        }

        private void CustomerViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CustomerViewModel.Name):
                    OnCanExecuteChanged();
                    break;
                default:
                    break;
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(customerViewModel.Name) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            int id;

            Customer customer = new Customer()
            {
                Name = customerViewModel.Name,
                LastName = customerViewModel.LastName,
                RegistrationDate = DateTime.Now
            };

            try
            {

                using (var client = new CustomerServiceClient())
                {
                    id = await client.AddAsync(customer);
                }
                customer.Id = id;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to create Customer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                if (customerViewModel.Movements.Any())
                {
                    MovementModelAdapter modelAdapter = customerViewModel.Movements.Peek();

                    Movement movement = new Movement()
                    {
                        Id = modelAdapter.Id,
                        Customer = customer,
                    };

                    using (var client = new MovementServiceClient(new InstanceContext(new MovementCallback((a) => { }))))
                    {
                        await client.UpdateAsync(movement);
                    }
                    customerViewModel.Movements.Dequeue();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to update Movement.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            


        }
    }
}
