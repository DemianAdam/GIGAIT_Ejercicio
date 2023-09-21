using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModels.Base;
using ViewModels.MovementService;

namespace ViewModels.CustomerViewModel.Commands
{
    public class SuscribeCommand : AsyncCommandBase
    {
        private CustomerViewModel customerViewModel;
        private MovementCallback movementCallback;
        private MovementServiceClient client;

        public SuscribeCommand(CustomerViewModel customerViewModel, MovementCallback movementCallback)
        {
            this.customerViewModel = customerViewModel;
            this.movementCallback = movementCallback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                client = new MovementServiceClient(movementCallback.InstanceContext);
                await client.SubscribeAsync();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to suscribe customer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                IEnumerable<Movement> movements = (await client.SelectAllAsync()).Where(x => x.Customer is null);
                foreach (Movement movement in movements) 
                {
                   await client.PublishAsync(movement);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to get movements.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
