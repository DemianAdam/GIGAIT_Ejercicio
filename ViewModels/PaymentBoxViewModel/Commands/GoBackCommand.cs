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

namespace ViewModels.Commands
{
    public class GoBackCommand : AsyncCommandBase
    {
        private readonly NavigationService navigationService;
        private readonly PaymentBoxModelAdapter paymentBox;

        public GoBackCommand(PaymentBoxModelAdapter paymentBox, NavigationService navigationService)
        {
            this.paymentBox = paymentBox;
            this.navigationService = navigationService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                using (var client = new PaymentBoxServiceClient())
                {
                    await client.DeactivateAsync(paymentBox.Id);
                }
                paymentBox.IsActive = false;
                navigationService.Navigate();
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
