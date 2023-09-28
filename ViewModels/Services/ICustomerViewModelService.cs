using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.CustomerService;
using ViewModels.MovementService;

namespace ViewModels.Services
{
    public interface ICustomerViewModelService
    {
        IMovementService MovementService { get; }
        ICustomerService CustomerService { get; }

        Task<IEnumerable<Movement>> GetMovementsAsync();

        Task<IEnumerable<Customer>> GetUnattendedCustomersAsync();
    }
}