using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.MovementService;
using ViewModels.PaymentBoxService;
using ViewModels.Services;

namespace ViewModels.Adapters
{
    /// <summary>
    /// PaymentBoxViewParameter model. This model represents a parameter for the PaymentBoxView.
    /// </summary>
    public class PaymentBoxViewParameter
    {
        public PaymentBoxViewParameter(PaymentBoxModelAdapter paymentBox, PaymentBoxViewModelServiceParameter serviceParameter)
        {
            PaymentBox = paymentBox;
            Movements = new List<MovementModelAdapter>();
            ServiceParameter = serviceParameter;
        }

        public PaymentBoxViewParameter(PaymentBoxModelAdapter paymentBox, IEnumerable<MovementModelAdapter> movements, PaymentBoxViewModelServiceParameter serviceParameter)
        {
            PaymentBox = paymentBox;
            Movements = movements;
            ServiceParameter = serviceParameter;
        }

        public PaymentBoxModelAdapter PaymentBox { get; }
        public IEnumerable<MovementModelAdapter> Movements { get; }
        public PaymentBoxViewModelServiceParameter ServiceParameter { get; }
    }
}
