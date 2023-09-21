using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Adapters
{
    public class PaymentBoxViewParameter
    {
        public PaymentBoxViewParameter(PaymentBoxModelAdapter paymentBox)
        {
            PaymentBox = paymentBox;
            Movements = new List<MovementModelAdapter>();
        }

        public PaymentBoxViewParameter(PaymentBoxModelAdapter paymentBox, IEnumerable<MovementModelAdapter> movements)
        {
            PaymentBox = paymentBox;
            Movements = movements;
        }

        public PaymentBoxModelAdapter PaymentBox { get; }
        public IEnumerable<MovementModelAdapter> Movements { get; }
    }
}
