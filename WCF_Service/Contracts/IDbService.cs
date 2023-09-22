using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Service.Contracts
{
    /// <summary>
    /// IDbService interface. This interface represents the contract for the DbService.
    /// </summary>
    [ServiceContract]
    public interface IDbService
    {
        [OperationContract]
        void ResetDatabase();
    }
}
