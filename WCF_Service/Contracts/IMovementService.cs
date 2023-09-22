using Models;
using System.Collections.Generic;
using System.ServiceModel;
using WCF_Service.Contracts;

namespace WCF_Service.Contracts
{
    /// <summary>
    /// MovementService interface. This interface defines the methods for the MovementService.
    /// This interface inherits from ICRUDService and IPubSubService. And has a callback contract of IPubSubCallback.
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IPubSubCallback))]
    public interface IMovementService :ICRUDService<Movement>, IPubSubService<Movement>
    {
    }
}