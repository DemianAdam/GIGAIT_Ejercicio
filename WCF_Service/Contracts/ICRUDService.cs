using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel;
using WCF_Service.Faults;

namespace WCF_Service.Contracts
{
    /// <summary>
    /// ICRUDService interface. This interface represents the CRUD service.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    [ServiceContract]
    public interface ICRUDService<T> where T : class
    {
        [FaultContract(typeof(SqlUniqueFault))]
        [OperationContract]
        int Add(T entity);

        [OperationContract]
        void Update(T entity);

        [OperationContract]
        void Delete(T entity);

        [OperationContract]
        IEnumerable<T> SelectAll();

        [OperationContract]
        T SelectById(int id);
    }
}
