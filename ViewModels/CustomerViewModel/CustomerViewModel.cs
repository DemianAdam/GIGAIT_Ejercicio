using Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModels.Base;
using ViewModels.CustomerService;
using ViewModels.CustomerViewModel.Commands;
using ViewModels.MovementService;
using ViewModels.Services;

namespace ViewModels.CustomerViewModel
{
    /// <summary>
    /// CustomerViewModel class. This class represents the customer view model.
    /// </summary>
    public class CustomerViewModel : ViewModelBase
    {
        private List<MovementModelAdapter> firstMovements = new List<MovementModelAdapter>();
        public ObservableQueue<MovementModelAdapter> Movements { get; }
        public ObservableQueue<CustomerModelAdapter> Customers { get; }



        /// <summary>
        /// FirstMovement property. This property represents the first movement.
        /// </summary>
        public string FirstMovement
        {
            get => GetMovementString(1);
        }

        public string SecondMovement
        {
            get => GetMovementString(2);
        }

        public string ThirdMovement
        {
            get => GetMovementString(3);
        }

        public string FourthMovement
        {
            get => GetMovementString(4);
        }

        private string GetMovementString(int index)
        {
            index--;
            if (firstMovements.Count > index)
            {
                return $"{firstMovements[index].Customer?.Name} Pase a la caja: {firstMovements[index].PaymentBox.Name}";
            }
            return string.Empty;
        }

        private string customerName;
        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }

        private string customerLastName;
        private readonly ICustomerViewModelService customerViewModelService;

        public string CustomerLastName
        {
            get
            {
                return customerLastName;
            }
            set
            {
                customerLastName = value;
                OnPropertyChanged(nameof(CustomerLastName));
            }
        }

        public ICommand CreateCustomerCommand { get; }

        public CustomerViewModel(ICustomerViewModelService customerViewModelService)
        {
            this.customerViewModelService = customerViewModelService;
            Customers = new ObservableQueue<CustomerModelAdapter>();
            Movements = new ObservableQueue<MovementModelAdapter>();
            CreateCustomerCommand = new CreateCustomerCommand(this, customerViewModelService.CustomerService);
            Customers.CollectionChanged += Customers_CollectionChanged;
            Movements.CollectionChanged += Movements_CollectionChanged;
        }

        private void Movements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        private void Customers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                //CustomerModelAdapter customer = e.NewItems[0] as CustomerModelAdapter;

                /*TODO: Implement this using DI and Repository pattern
                try
                {
                    if (customerViewModel.Movements.Any())
                    {
                        MovementModelAdapter modelAdapter = customerViewModel.Movements.Peek();

                        Movement movement = new Movement()
                        {
                            Id = modelAdapter.Id,
                            Customer = customer,
                        };

                        using (var client = new MovementServiceClient(new InstanceContext(new MovementCallback((a) => { }))))
                        {
                            await client.UpdateAsync(movement);
                        }
                        customerViewModel.Movements.Pop();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to update Movement.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                */
            }
        }


        public async static Task<CustomerViewModel> LoadCustomerViewModel(ICustomerViewModelService customerViewModelService)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel(customerViewModelService);

            try
            {
                var movements = await customerViewModelService.MovementService.SelectAllAsync();
                customerViewModel.UpdateMovements(movements);
            }
            catch
            {
                MessageBox.Show("Failed to get movements.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                var customers = await customerViewModelService.CustomerService.SelectAllUnattendedCustomersAsync();
                customerViewModel.UpdateCustomers(customers);
            }
            catch
            {
                MessageBox.Show("Failed to get customers.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return customerViewModel;
        }

        public void UpdateMovements(IEnumerable<Movement> movements)
        {
            foreach (var movement in movements)
            {
                /* if (Customers.Any())
                 {
                     Customer customer = Customers.Dequeue().ToCustomer();
                     movement.Customer = customer;
                 }*/
                this.Movements.Enqueue(new MovementModelAdapter(movement));
            }
            firstMovements = this.Movements.Take(4).ToList();
            OnPropertyChanged(nameof(FirstMovement));
            OnPropertyChanged(nameof(SecondMovement));
            OnPropertyChanged(nameof(ThirdMovement));
            OnPropertyChanged(nameof(FourthMovement));
        }

        public override void Dispose()
        {
            Customers.CollectionChanged -= Customers_CollectionChanged;
            Movements.CollectionChanged -= Movements_CollectionChanged;
            base.Dispose();
        }

        public void UpdateCustomers(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Customers.Enqueue(new CustomerModelAdapter(customer));
            }
        }
    }
}
