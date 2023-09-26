﻿using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModels.Adapters;
using ViewModels.Base;
using ViewModels.MovementService;
using ViewModels.PaymentBoxService;
using ViewModels.Services;
using ViewModels.Stores;

namespace ViewModels.Commands
{ 
    /// <summary>
    /// LoginCommand class. This class represents the command for the LoginView.
    /// </summary>
    public class LoginCommand : AsyncCommandBase
    {
        private readonly ParameterNavigationService<PaymentBoxViewParameter> paymentBoxViewNavigationService;
        private readonly LoginViewModel loginViewModel;

        public override bool CanExecute(object parameter)
        {
            return loginViewModel.PaymentBoxes.Any() && loginViewModel.PaymentBox != null && base.CanExecute(parameter);
        }


        /// <summary>
        /// This method activates the payment box and navigates to the payment box view.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                PaymentBoxViewParameter param;
                InstanceContext context = new InstanceContext(new MovementCallback());
                using (var client = new MovementServiceClient(context))
                {
                    var result = await client.SelectAllAsync();
                    IEnumerable<MovementModelAdapter> movements = result.Where(x => x.PaymentBox.Id == loginViewModel.PaymentBox.Id).Select(x => new MovementModelAdapter(x));
                    param = new PaymentBoxViewParameter(loginViewModel.PaymentBox, movements);
                }
                using (var client = new PaymentBoxServiceClient())
                {
                    await client.ActivateAsync(loginViewModel.PaymentBox.Id);
                }
                
                paymentBoxViewNavigationService.Navigate(param);
                param.PaymentBox.IsActive = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load Movements.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public LoginCommand(LoginViewModel loginViewModel, ParameterNavigationService<PaymentBoxViewParameter> paymentBoxViewNavigationService)
        {
            this.loginViewModel = loginViewModel;
            this.paymentBoxViewNavigationService = paymentBoxViewNavigationService;

            loginViewModel.PropertyChanged += OnLoginViewModel_PropertyChanged;
        }

        private void UpdateMovements(Queue<Movement> value)
        {
            //loginViewModel.UpdateMovements(value);
        }

        private void OnLoginViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(LoginViewModel.PaymentBox):
                case nameof(LoginViewModel.SelectedIndex):
                    OnCanExecuteChanged();
                    break;
                default:
                    break;
            }
        }
    }
}
