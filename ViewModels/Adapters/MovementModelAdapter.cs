using Models;
using System;

namespace ViewModels
{
    public class MovementModelAdapter : ViewModelBase
    {
        private readonly Movement movement;
        public int Id => movement.Id;
        public CustomerModelAdapter Customer { get; }
        public PaymentBoxModelAdapter PaymentBox { get; }
        public DateTime CreationDate => movement.CreationDate;
        public DateTime? OcurredDate => movement.OcurredDate;

        public MovementModelAdapter(Movement movement)
        {
            this.movement = movement;
            if (!(movement.Customer is null))
            {
                Customer = new CustomerModelAdapter(movement.Customer);
            }
            if (!(movement.PaymentBox is null))
            {
                PaymentBox = new PaymentBoxModelAdapter(movement.PaymentBox);
            }
        }

        public override string ToString()
        {
            return $"Id: {Id}, Customer: {Customer}, PaymentBox: {PaymentBox}, CreationDate: {CreationDate}, OcurredDate: {OcurredDate}";
        }
    }
}