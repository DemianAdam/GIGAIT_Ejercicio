using Models;

namespace ViewModels
{
    public class PaymentBoxModelAdapter : ViewModelBase
    {
        private readonly PaymentBox paymentBox;
        public int Id => paymentBox.Id;
        public string Name => paymentBox.Name;
        public bool IsActive { get;  set; }

        public PaymentBoxModelAdapter(PaymentBox paymentBox)
        {
            this.paymentBox = paymentBox;
            this.IsActive = paymentBox.IsActive;
        }

        public override string ToString()
        {
            return $"Name: {Name}";
        }

        public override bool Equals(object obj)
        {
            if (obj is PaymentBoxModelAdapter other)
            {
                return this.Id == other.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}