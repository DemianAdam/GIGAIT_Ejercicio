using Models;
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
        private readonly PaymentBoxViewModelServiceParameter serviceParameter;


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
                var result = await serviceParameter.MovementService.SelectAllAsync();
                IEnumerable<MovementModelAdapter> movements = result.Where(x => x.PaymentBox.Id == loginViewModel.PaymentBox.Id)
                    .Select(x => new MovementModelAdapter(x));
                PaymentBoxViewParameter param = new PaymentBoxViewParameter(loginViewModel.PaymentBox, movements, serviceParameter);

                await serviceParameter.PaymentBoxService.ActivateAsync(loginViewModel.PaymentBox.Id);

                paymentBoxViewNavigationService.Navigate(param);
                param.PaymentBox.IsActive = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load Movements.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                loginViewModel.PropertyChanged -= OnLoginViewModel_PropertyChanged;
            }
        }

        public LoginCommand(LoginViewModel loginViewModel, ParameterNavigationService<PaymentBoxViewParameter> paymentBoxViewNavigationService, PaymentBoxViewModelServiceParameter serviceParameter)
        {
            this.serviceParameter = serviceParameter;
            this.loginViewModel = loginViewModel;
            this.paymentBoxViewNavigationService = paymentBoxViewNavigationService;
            loginViewModel.PropertyChanged += OnLoginViewModel_PropertyChanged;
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
