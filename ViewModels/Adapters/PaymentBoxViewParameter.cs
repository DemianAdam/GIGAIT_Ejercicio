using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.MovementService;
using ViewModels.PaymentBoxService;

namespace ViewModels.Adapters
{
    /// <summary>
    /// PaymentBoxViewParameter model. This model represents a parameter for the PaymentBoxView.
    /// </summary>
    public class PaymentBoxViewParameter
    {
        public PaymentBoxViewParameter(PaymentBoxModelAdapter paymentBox, ServiceParameter serviceParameter)
        {
            PaymentBox = paymentBox;
            Movements = new List<MovementModelAdapter>();
            ServiceParameter = serviceParameter;
        }

        public PaymentBoxViewParameter(PaymentBoxModelAdapter paymentBox, IEnumerable<MovementModelAdapter> movements, ServiceParameter serviceParameter)
        {
            PaymentBox = paymentBox;
            Movements = movements;
            ServiceParameter = serviceParameter;
        }

        public PaymentBoxModelAdapter PaymentBox { get; }
        public IEnumerable<MovementModelAdapter> Movements { get; }
        public ServiceParameter ServiceParameter { get; }
    }
}
