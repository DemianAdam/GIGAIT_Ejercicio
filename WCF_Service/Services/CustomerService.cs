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
    /// <summary>
    /// CustomerService class. This class represents the service for the Customer model.
    /// </summary>
    public partial class CustomerService : ICustomerService
    {
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

        public void Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

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

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
