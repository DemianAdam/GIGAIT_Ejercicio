using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Base;
using ViewModels.CustomerViewModel.Commands;

namespace ViewModels.CustomerViewModel
{
    public class CustomerViewModel : ViewModelBase
    {
        private readonly MovementCallback movementCallback;
        private readonly ObservableQueue<MovementModelAdapter> movements;
        private readonly ObservableQueue<CustomerModelAdapter> customers;
        private List<MovementModelAdapter> firstMovements = new List<MovementModelAdapter>();
        public Queue<MovementModelAdapter> Movements => new Queue<MovementModelAdapter>(movements);
        public Queue<CustomerModelAdapter> Customers => new Queue<CustomerModelAdapter>(customers);

        public string FirstMovement
        {
            get
            {
                if (firstMovements.Count > 0)
                {
                    return $"{firstMovements[0].Customer?.Name} Pase a la caja: {firstMovements[0].PaymentBox.Name}";
                }
                return string.Empty;
            }
        }

        public string SecondMovement
        {
            get
            {
                if (firstMovements.Count > 1)
                {
                    return $"{firstMovements[1].Customer?.Name} Pase a la caja: {firstMovements[1].PaymentBox.Name}";
                }
                return string.Empty;
            }
        }

        public string ThirdMovement
        {
            get
            {
                if (firstMovements.Count > 2)
                {
                    return $"{firstMovements[2].Customer?.Name} Pase a la caja: {firstMovements[2].PaymentBox.Name}";
                }
                return string.Empty;
            }
        }

        public string FourthMovement
        {
            get
            {
                if (firstMovements.Count > 3)
                {
                    return $"{firstMovements[3].Customer?.Name} Pase a la caja: {firstMovements[3].PaymentBox.Name}";
                }
                return string.Empty;
            }
        }




        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public ICommand CreateCustomerCommand { get; }

        public ICommand SuscribeCommand { get; }

        public CustomerViewModel()
        {
            this.customers = new ObservableQueue<CustomerModelAdapter>();
            movements = new ObservableQueue<MovementModelAdapter>();
            this.movementCallback = new MovementCallback(UpdateMovements);
            CreateCustomerCommand = new CreateCustomerCommand(this);
            SuscribeCommand = new SuscribeCommand(this, movementCallback);
            this.movements.CollectionChanged += Movements_CollectionChanged;
        }

        private void Movements_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Movements));
        }

        public static CustomerViewModel LoadCustomerViewModel()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customerViewModel.SuscribeCommand.Execute(null);
            return customerViewModel;
        }

        public void UpdateMovements(IEnumerable<Movement> movements)
        {
            this.movements.Clear();
            foreach (var movement in movements)
            {
                if (customers.Any())
                {
                    Customer customer =  customers.Dequeue().ToCustomer();
                    movement.Customer = customer;
                }
                this.movements.Enqueue(new MovementModelAdapter(movement));
            }
            firstMovements = this.movements.Take(4).ToList();
            OnPropertyChanged(nameof(FirstMovement));
            OnPropertyChanged(nameof(SecondMovement));
            OnPropertyChanged(nameof(ThirdMovement));
            OnPropertyChanged(nameof(FourthMovement));
        }

    }
}
