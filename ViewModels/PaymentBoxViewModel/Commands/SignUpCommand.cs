using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModels.Adapters;
using ViewModels.PaymentBoxService;
using ViewModels.Services;

namespace ViewModels.Commands
{
    /// <summary>
    /// SignUpCommand class. This class represents the command for signing up a new payment box.
    /// </summary>
    public class SignUpCommand : AsyncCommandBase
    {
        private readonly LoginViewModel loginViewModel;
        private readonly ParameterNavigationService<PaymentBoxViewParameter> paymentBoxViewNavigationService;
        private readonly ServiceParameter serviceParameter;

        override public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(loginViewModel.NewPaymentBoxName) && base.CanExecute(parameter);
        }

        public SignUpCommand(LoginViewModel loginViewModel, ParameterNavigationService<PaymentBoxViewParameter> paymentBoxViewNavigationService, ServiceParameter loginViewParameter)
        {
            this.serviceParameter = loginViewParameter;
            this.loginViewModel = loginViewModel;
            this.paymentBoxViewNavigationService = paymentBoxViewNavigationService;
            loginViewModel.PropertyChanged += OnLoginViewModel_PropertyChanged;
        }

        private void OnLoginViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.NewPaymentBoxName))
            {
                OnCanExecuteChanged();
            }
        }

        /// <summary>
        /// This method adds a new payment box and navigates to the payment box view.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                var paymentBox = new PaymentBox()
                {
                    Name = loginViewModel.NewPaymentBoxName,
                    IsActive = true
                };
                using (var client = new PaymentBoxServiceClient())
                {
                    paymentBox.Id = await client.AddAsync(paymentBox);
                }
                PaymentBoxViewParameter paymentBoxViewParameter = new PaymentBoxViewParameter(new PaymentBoxModelAdapter(paymentBox), serviceParameter);
                paymentBoxViewNavigationService.Navigate(paymentBoxViewParameter);
            }
            catch (FaultException<SqlUniqueFault> ex)
            {
                MessageBox.Show(ex.Detail.Details);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while adding new payment box.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}
