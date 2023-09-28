using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Stores;
using ViewModels.PaymentBoxService;
using ViewModels.Services;
using Models;
using System.ServiceModel;
using System.Windows;
using ViewModels.Adapters;

namespace ViewModels.Commands
{
    /// <summary>
    /// GoBackCommand class. This class represents the command for going back to the login view.
    /// </summary>
    public class GoBackCommand : AsyncCommandBase
    {
        private readonly ParameterNavigationService<PaymentBoxViewModelServiceParameter> navigationService;
        private readonly PaymentBoxViewParameter paymentBoxViewParameter;

        public GoBackCommand(ParameterNavigationService<PaymentBoxViewModelServiceParameter> navigationService, PaymentBoxViewParameter paymentBoxViewParameter)
        {
            this.paymentBoxViewParameter = paymentBoxViewParameter;
            this.navigationService = navigationService;
        }

        /// <summary>
        /// This method deactivates the payment box and navigates to the login view.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await paymentBoxViewParameter.ServiceParameter.PaymentBoxService.DeactivateAsync(paymentBoxViewParameter.PaymentBox.Id);
                paymentBoxViewParameter.PaymentBox.IsActive = false;

                PaymentBoxViewModelServiceParameter param = new PaymentBoxViewModelServiceParameter(paymentBoxViewParameter.ServiceParameter.MovementService, paymentBoxViewParameter.ServiceParameter.PaymentBoxService);
                navigationService.Navigate(param);
            }
            catch (FaultException<SqlInvalidOperationException> ex)
            {
                MessageBox.Show(ex.Detail.Details, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to deactivate payment box.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
