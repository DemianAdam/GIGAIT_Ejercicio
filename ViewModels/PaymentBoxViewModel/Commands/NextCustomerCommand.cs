﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                using (var client = new PaymentBoxServiceClient())
                {
                    paymentBox = await client.SelectByIdAsync(paymentBoxViewModel.Id);
                }

                Movement movement = new Movement()
                {
                    CreationDate = DateTime.Now,
                    PaymentBox = paymentBox
                };

                using (var client = new MovementServiceClient(new InstanceContext(new MovementCallback(UpdateMovements))))
                {
                    int id = await client.AddAsync(movement);
                    movement.Id = id;
                    await client.PublishAsync(movement);
                }
                paymentBoxViewModel.MovementsHistory.Push(new MovementModelAdapter(movement));
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to add movement", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateMovements(Queue<Movement> value)
        {

        }

        public NextCustomerCommand(PaymentBoxViewModel paymentBoxViewModel)
        {
            this.paymentBoxViewModel = paymentBoxViewModel;
        }
    }
}
