using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Models;
using WCF_Service.Contracts;

namespace WCF_Service.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.

    //[ServiceContract]
    public partial class CustomerService : ICustomerService
    {


        // [OperationContract(Name = "AddCustomer")]
        public int Add(Customer customer)
        {
            using (DbOperation context = new DbOperation())
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@name", customer.Name),
                    new SqlParameter("@last_name", customer.LastName ?? Convert.DBNull),
                    new SqlParameter("@registration_date", customer.RegistrationDate)
                };

                return Convert.ToInt32(context.ExecuteQueryScalar("AddCustomer", CommandType.StoredProcedure, parameters));
            }
        }

        //  [OperationContract(Name = "DeleteCustomer")]
        public void Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        // [OperationContract(Name = "SelectAllCustomers")]
        public IEnumerable<Customer> SelectAll()
        {
            List<Customer> customers = new List<Customer>();
            DataTable dataTable;

            using (DbOperation context = new DbOperation())
            {
                dataTable = context.ExecuteQuery("SelectUnattendedCustomers", CommandType.StoredProcedure);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                Customer customer = DbOperation.GetCustomer(row);
                customers.Add(customer);
            }

            return customers;
        }

        //  [OperationContract(Name = "SelectAllUnattendedCustomers")]
        public IEnumerable<Customer> SelectAllUnattendedCustomers()
        {
            List<Customer> customers = new List<Customer>();
            DataTable dataTable;

            using (DbOperation context = new DbOperation())
            {
                dataTable = context.ExecuteQuery("SelectUnattendedCustomers", CommandType.StoredProcedure);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                Customer customer = DbOperation.GetCustomer(row);
                customers.Add(customer);
            }

            return customers;
        }

        // [OperationContract(Name = "SelectCustomerById")]
        public Customer SelectById(int id)
        {
            Customer customer = new Customer();

            using (DbOperation context = new DbOperation())
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", id)
                };

                var dataTable = context.ExecuteQuery("SelectCustomerById", CommandType.StoredProcedure, parameters);

                customer = dataTable.Rows.Count > 0 ? DbOperation.GetCustomer(dataTable.Rows[0]) : null;
            }

            return customer;
        }

        //  [OperationContract(Name = "UpdateCustomer")]
        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
