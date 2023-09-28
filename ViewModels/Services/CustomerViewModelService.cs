using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.CustomerService;
using ViewModels.MovementService;

namespace ViewModels.Services
{
    public class CustomerViewModelService : ICustomerViewModelService
    {
        public IMovementService MovementService { get; }
        public ICustomerService CustomerService { get; }

        public CustomerViewModelService(IMovementService movementService, ICustomerService customerService)
        {
            MovementService = movementService;
            CustomerService = customerService;
        }

        public async Task<IEnumerable<Movement>> GetMovementsAsync()
        {
            return await MovementService.SelectAllAsync();
        }

        public async Task<IEnumerable<Customer>> GetUnattendedCustomersAsync()
        {
            return await CustomerService.SelectAllUnattendedCustomersAsync();
        }
    }
}
