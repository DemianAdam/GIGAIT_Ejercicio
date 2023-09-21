using Models;
using System.ServiceModel;
using WCF_Service.Faults;

namespace WCF_Service.Contracts
{
    [ServiceContract]
    public interface IPaymentBoxService : ICRUDService<PaymentBox>
    {
        [FaultContract(typeof(SqlInvalidOperationException))]
        [OperationContract]
        void Activate(int paymentBoxId);

        [FaultContract(typeof(SqlInvalidOperationException))]
        [OperationContract]
        void Deactivate(int paymentBoxId);

        [OperationContract]
        int SelectLastId();

        [OperationContract]
        int SelectIdByName(string name);
    }
}