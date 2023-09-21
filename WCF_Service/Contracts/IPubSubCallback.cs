using Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace WCF_Service.Contracts
{
    public interface IPubSubCallback
    {
        [OperationContract(IsOneWay = true)]
        void MovementsChanged(Queue<Movement> value);
    }
}