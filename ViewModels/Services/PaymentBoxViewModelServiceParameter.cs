using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.MovementService;
using ViewModels.PaymentBoxService;

namespace ViewModels.Services
{
    public class PaymentBoxViewModelServiceParameter
    {
        public PaymentBoxViewModelServiceParameter(IMovementService movementService, IPaymentBoxService paymentBoxService)
        {
            MovementService = movementService;
            PaymentBoxService = paymentBoxService;
        }

        public IMovementService MovementService { get; }
        public IPaymentBoxService PaymentBoxService { get; }

    }
}
