using Models;
using System;

namespace ViewModels
{
    public class CustomerModelAdapter : ViewModelBase
    {
        private readonly Customer customer;
        public int? Id => customer?.Id;
        public string Name => customer?.Name;
        public string LastName => customer?.LastName;
        public DateTime? RegistrationDate => customer?.RegistrationDate;
        public string FullName => $"{Name} {LastName}";

        public CustomerModelAdapter(Customer customer)
        {
            this.customer = customer;
        }

        public override string ToString()
        {
            return $"Id: {Id} | Name: {Name} | LastName: {LastName} | RegistrationDate: {RegistrationDate}";
        }

        internal Customer ToCustomer()
        {
            return new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                LastName = customer.LastName,
                RegistrationDate = customer.RegistrationDate
            };
        }
    }
}