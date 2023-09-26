using Models;
using System;
using System.Collections.Generic;
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
    /// SuscribeCommand class. This class represents the command for suscribe a customer.
    /// </summary>
    public class SuscribeCommand : AsyncCommandBase
    {
        private CustomerViewModel customerViewModel;
        private readonly IMovementService movementService;

        public SuscribeCommand(CustomerViewModel customerViewModel, IMovementService movementService)
        {
            this.movementService = movementService;
            this.customerViewModel = customerViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                movementService.Subscribe();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to suscribe customer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                IEnumerable<Movement> movements = (await movementService.SelectAllAsync()).Where(x => x.Customer is null);
                customerViewModel.UpdateMovements(movements);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to get movements.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
