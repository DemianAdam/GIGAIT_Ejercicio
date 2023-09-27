using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Commands;
using System.Xml;
using ViewModels.Stores;
using System.Collections.Specialized;
using ViewModels.Services;
using ViewModels.Adapters;
using ViewModels.MovementService;
using ViewModels.PaymentBoxService;

namespace ViewModels
{
    /// <summary>
    /// LoginViewModel class. This class represents the login view model.
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        private readonly ObservableCollection<PaymentBoxModelAdapter> paymentBoxes;

        public IEnumerable<PaymentBoxModelAdapter> PaymentBoxes => paymentBoxes;

        private PaymentBoxModelAdapter paymentBox;
        public PaymentBoxModelAdapter PaymentBox
        {
            get
            {
                return paymentBox;
            }
            set
            {
                paymentBox = value;
                OnPropertyChanged(nameof(PaymentBox));
            }
        }
        public ICommand LoginCommand { get; }

        public ICommand SignUpCommand { get; }

        //TODO: Implement reposytory pattern insead of using the following command.
        public ICommand LoadPaymentBoxesCommand { get; }

        private int selectedIndex = -1;

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;
                    OnPropertyChanged(nameof(SelectedIndex));
                }
            }
        }

        public LoginViewModel(ParameterNavigationService<PaymentBoxViewParameter> paymentBoxViewNavigationService, ServiceParameter loginViewParameter)
        {
            this.paymentBoxes = new ObservableCollection<PaymentBoxModelAdapter>();
            this.paymentBoxes.CollectionChanged += PaymentBoxes_CollectionChanged;
            LoadPaymentBoxesCommand = new LoadPaymentBoxesCommand(this);
            LoginCommand = new LoginCommand(this, paymentBoxViewNavigationService, loginViewParameter);
            SignUpCommand = new SignUpCommand(this, paymentBoxViewNavigationService, loginViewParameter);
        }

        private void PaymentBoxes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (paymentBoxes.Count > 0)
            {
                SelectedIndex = 0;
            }
            else
            {
                SelectedIndex = -1;
            }
        }

        public static LoginViewModel LoadLoginViewModel(ParameterNavigationService<PaymentBoxViewParameter> navigationService, ServiceParameter loginViewParameter)
        {
            LoginViewModel loginViewModel = new LoginViewModel(navigationService, loginViewParameter);
            loginViewModel.LoadPaymentBoxesCommand.Execute(null);
            return loginViewModel;
        }

        private string newPaymentBoxName;
        public string NewPaymentBoxName
        {
            get
            {
                return newPaymentBoxName;
            }
            set
            {
                newPaymentBoxName = value;
                OnPropertyChanged(nameof(NewPaymentBoxName));
            }
        }

        public void UpdatePaymentBoxes(IEnumerable<PaymentBoxModelAdapter> paymentBoxes)
        {
            this.paymentBoxes.Clear();
            foreach (var paymentBox in paymentBoxes)
            {
                this.paymentBoxes.Add(paymentBox);
            }
        }

        public override void Dispose()
        {
            this.paymentBoxes.CollectionChanged -= PaymentBoxes_CollectionChanged;
            base.Dispose();
        }

    }
}
