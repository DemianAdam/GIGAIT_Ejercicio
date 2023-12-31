﻿using Models;
using System;

namespace ViewModels
{
    /// <summary>
    /// CustomerModelAdapter class. This class is an adapter for the Customer model.
    /// </summary>
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
    }
}