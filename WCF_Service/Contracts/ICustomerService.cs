using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Service.Contracts
{
    /// <summary>
    /// ICustomerService interface. This interface contains the methods that the CustomerService implements.
    /// </summary>
    [ServiceContract]
    public interface ICustomerService : ICRUDService<Customer>
    {
        [OperationContract]
        IEnumerable<Customer> SelectAllUnattendedCustomers();
    }
}
