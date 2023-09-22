    using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModels.PaymentBoxService;

namespace ViewModels.Commands
{
    /// <summary>
    /// LoadPaymentBoxesCommand class. This class represents the command for loading payment boxes.
    /// </summary>
    public class LoadPaymentBoxesCommand : AsyncCommandBase
    {
        private readonly LoginViewModel loginViewModel;

        public LoadPaymentBoxesCommand(LoginViewModel loginViewModel)
        {
            this.loginViewModel = loginViewModel;
        }

        /// <summary>
        /// This method loads the payment boxes.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                using (var client = new PaymentBoxServiceClient())
                {
                    var result = await client.SelectAllAsync();
                    IEnumerable<PaymentBoxModelAdapter> paymentBoxes = result.Where(x =>x.IsActive == false).Select(x => new PaymentBoxModelAdapter(x));
                    loginViewModel.UpdatePaymentBoxes(paymentBoxes.ToList());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load payment boxes.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
