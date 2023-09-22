using Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace WCF_Service.Contracts
{
    /// <summary>
    /// IPubSubCallback interface. This interface represents the callback for the PubSubService.
    /// </summary>
    public interface IPubSubCallback
    {
        [OperationContract(IsOneWay = true)]
        void MovementsChanged(Queue<Movement> value);
    }
}