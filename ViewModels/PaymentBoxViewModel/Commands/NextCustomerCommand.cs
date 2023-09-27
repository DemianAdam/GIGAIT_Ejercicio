using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModels.Adapters;
using ViewModels.Base;
using ViewModels.MovementService;
using ViewModels.PaymentBoxService;

namespace ViewModels.Commands
{
    /// <summary>
    /// NextCustomerCommand class. This class represents the command for the button that adds a new customer to the queue.
    /// </summary>
    public class NextCustomerCommand : AsyncCommandBase
    {
        private readonly PaymentBoxViewModel paymentBoxViewModel;
        private readonly ServiceParameter serviceParameter;
        private readonly IMovementService movementService;
        private readonly IPaymentBoxService paymentBoxService;

        public override bool CanExecute(object parameter)
        {
            if (paymentBoxViewModel.MovementsHistory.Count > 0)
            {
                CustomerModelAdapter customer = paymentBoxViewModel.MovementsHistory.Peek().Customer;

                return customer != null && base.CanExecute(parameter);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// This method adds a new customer to the queue and publishes the movement.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                PaymentBox paymentBox;
                paymentBox = await serviceParameter.PaymentBoxService.SelectByIdAsync(paymentBoxViewModel.Id);
                Movement movement = new Movement()
                {
                    CreationDate = DateTime.Now,
                    PaymentBox = paymentBox
                };
                movement.Id = await serviceParameter.MovementService.AddAsync(movement);
                await serviceParameter.MovementService.PublishAsync(movement);
                paymentBoxViewModel.MovementsHistory.Push(new MovementModelAdapter(movement));
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to add movement", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public NextCustomerCommand(PaymentBoxViewModel paymentBoxViewModel, ServiceParameter serviceParameter)
        {
            this.paymentBoxViewModel = paymentBoxViewModel;
            this.serviceParameter = serviceParameter;
        }
    }
}
