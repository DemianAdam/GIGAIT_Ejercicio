using Models;
using System.Collections.Generic;
using System.ServiceModel;
using WCF_Service.Contracts;

namespace WCF_Service.Contracts
{
    [ServiceContract(CallbackContract = typeof(IPubSubCallback))]
    public interface IMovementService :ICRUDService<Movement>, IPubSubService<Movement>
    {
    }
}