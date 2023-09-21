﻿using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Adapters;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;
using System.ComponentModel;
using System.Windows;
using ViewModels.PaymentBoxService;

namespace ViewModels
{
    public class PaymentBoxViewModel : ViewModelBase
    {
        private readonly ObservableStack<MovementModelAdapter> movementsHistory;
        private readonly PaymentBoxModelAdapter paymentBox;
        public int Id => paymentBox.Id;
        public string Name => "Caja de pago: " + paymentBox.Name;
        public ICommand NextCustomerCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand ReloadCommand { get; }
        public ObservableStack<MovementModelAdapter> MovementsHistory => movementsHistory;

        public PaymentBoxViewModel(NavigationService navigationService, PaymentBoxViewParameter paymentBoxViewParameter)
        {
            this.paymentBox = paymentBoxViewParameter.PaymentBox;
            this.movementsHistory = new ObservableStack<MovementModelAdapter>(paymentBoxViewParameter.Movements);
            NextCustomerCommand = new NextCustomerCommand(this);
            GoBackCommand = new GoBackCommand(this.paymentBox, navigationService);
        }

        ~PaymentBoxViewModel()
        {
            if (paymentBox.IsActive)
            {
                using (var client = new PaymentBoxServiceClient())
                {
                     client.Deactivate(paymentBox.Id);
                }
            }
        }
    }
}