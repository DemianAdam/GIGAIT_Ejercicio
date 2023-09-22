using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Models;

namespace WCF_Service
{
    /// <summary>
    /// DbOperation class. This class represents a database operation.
    /// </summary>
    internal class DbOperation : IDisposable
    {
        private SqlConnection connection;

        public DbOperation(string connectionString = null)
        {
            if (connectionString is null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }

            connection = new SqlConnection(connectionString);
        }

        private void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        public int ExecuteNonQuery(string sqlQuery, CommandType commandType, SqlParameter[] parameters = null)
        {
            OpenConnection();
            using (var command = new SqlCommand(sqlQuery, connection))
            {
                command.CommandType = commandType;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }
                int rowsAffected = command.ExecuteNonQuery();
                CloseConnection();
                return rowsAffected;
            }
        }

        public DataTable ExecuteQuery(string sqlQuery, CommandType commandType, SqlParameter[] parameters = null)
        {
            OpenConnection();
            using (var command = new SqlCommand(sqlQuery, connection))
            {
                command.CommandType = commandType;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }
                var dataTable = new DataTable();
                var dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
                CloseConnection();
                return dataTable;
            }
        }

        public object ExecuteQueryScalar(string sqlQuery, CommandType commandType, SqlParameter[] parameters = null)
        {
            OpenConnection();
            using (var command = new SqlCommand(sqlQuery, connection))
            {
                command.CommandType = commandType;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }
                object result = command.ExecuteScalar();
                CloseConnection();
                return result;
            }
        }

        public void Dispose()
        {
            CloseConnection();

            if (connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }

        public static Movement GetMovement(DataRow row)
        {
            Customer customer = GetCustomer(row);
            PaymentBox paymentBox = GetPaymentBox(row);
            Movement movement = new Movement();
            movement.Id = Convert.ToInt32(row["movement_id"]);
            movement.Customer = customer;
            movement.PaymentBox = paymentBox;
            movement.CreationDate = Convert.ToDateTime(row["creation_date_time"]);
            movement.OcurredDate = row["ocurred_date_time"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["ocurred_date_time"]);
            return movement;
        }

        public static Customer GetCustomer(DataRow row)
        {
            Customer customer = null;

            if (row["customer_id"] != DBNull.Value)
            {
                customer = new Customer();
                customer.Id = Convert.ToInt32(row["customer_id"]);
                customer.Name = row["customer_name"].ToString();
                customer.LastName = row["customer_last_name"] == DBNull.Value ? null : row["customer_last_name"].ToString();
                customer.RegistrationDate = Convert.ToDateTime(row["customer_registration_date"]);
            }

            return customer;
        }

        public static PaymentBox GetPaymentBox(DataRow row)
        {
            PaymentBox paymentBox = new PaymentBox();
            paymentBox.Id = Convert.ToInt32(row["payment_box_id"]);
            paymentBox.Name = row["payment_box_name"].ToString();
            paymentBox.IsActive = Convert.ToBoolean(row["payment_box_is_active"]);
            return paymentBox;
        }
    }
}
