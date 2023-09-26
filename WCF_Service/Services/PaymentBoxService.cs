using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF_Service.Contracts;
using WCF_Service.Faults;

namespace WCF_Service.Services
{
    /// <summary>
    /// PaymentBoxService class. This class represents the service for the PaymentBox model.
    /// </summary>
    public partial class PaymentBoxService : IPaymentBoxService
    {
        public void Activate(int paymentBoxId)
        {
            using (var context = new DbOperation())
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", paymentBoxId),
                };
                try
                {
                    context.ExecuteNonQuery("ActivatePaymentBox", CommandType.StoredProcedure, parameters);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 51000)
                    {
                        SqlInvalidOperationException sqlFault = new SqlInvalidOperationException()
                        {
                            Message = ex.Message,
                            Details = $"Payment box is already active. Id: {paymentBoxId}"
                        };

                        throw new FaultException<SqlInvalidOperationException>(sqlFault);
                    }
                }
            }
        }


        public int Add(PaymentBox paymentBox)
        {
            int result = -1;
            using (var context = new DbOperation())
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@name", paymentBox.Name),
                    new SqlParameter("@is_active", paymentBox.IsActive)
                };


                try
                {
                    result = Convert.ToInt32(context.ExecuteQueryScalar("AddPaymentBox", CommandType.StoredProcedure, parameters));
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        SqlUniqueFault sqlFault = new SqlUniqueFault()
                        {
                            Message = ex.Message,
                            Details = $"Payment box with this name already exists. Name: {paymentBox.Name}"
                        };

                        throw new FaultException<SqlUniqueFault>(sqlFault);
                    }

                }


            }

            return result;
        }

        public void Deactivate(int paymentBoxId)
        {
            using (var context = new DbOperation())
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", paymentBoxId),
                };
                try
                {
                    context.ExecuteNonQuery("DeactivatePaymentBox", CommandType.StoredProcedure, parameters);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 51001)
                    {
                        SqlInvalidOperationException sqlFault = new SqlInvalidOperationException()
                        {
                            Message = ex.Message,
                            Details = $"Payment box is not active. Id: {paymentBoxId}"
                        };

                        throw new FaultException<SqlInvalidOperationException>(sqlFault);
                    }
                }
            }
        }

        // [OperationContract(Name = "DeletePaymentBox")]
        public void Delete(PaymentBox entity)
        {
            throw new NotImplementedException();
        }

        public int SelectIdByName(string name)
        {
            using (var context = new DbOperation())
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@name", name)
                };
                return Convert.ToInt32(context.ExecuteQueryScalar("SelectPaymentBoxIdByName", CommandType.StoredProcedure, parameters));
            }
        }

        public int SelectLastId()
        {
            using (var context = new DbOperation())
            {
                return Convert.ToInt32(context.ExecuteQueryScalar("SelectLastPaymentBoxId", CommandType.StoredProcedure));
            }
        }

        //  [OperationContract(Name = "UpdatePaymentBox")]
        public void Update(PaymentBox entity)
        {
            throw new NotImplementedException();
        }

        // [OperationContract(Name = "SelectAllPaymentBoxes")]
        IEnumerable<PaymentBox> ICRUDService<PaymentBox>.SelectAll()
        {
            List<PaymentBox> paymentBoxes = new List<PaymentBox>();

            using (var context = new DbOperation())
            {
                var dataTable = context.ExecuteQuery("SelectPaymentBoxes", CommandType.StoredProcedure);

                foreach (DataRow row in dataTable.Rows)
                {
                    PaymentBox paymentBox = DbOperation.GetPaymentBox(row);
                    paymentBoxes.Add(paymentBox);
                }
            }

            return paymentBoxes;
        }

        // [OperationContract(Name = "SelectPaymentBoxById")]
        PaymentBox ICRUDService<PaymentBox>.SelectById(int id)
        {
            PaymentBox paymentBox = new PaymentBox();

            using (var context = new DbOperation())
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", id),
                };

                var dataTable = context.ExecuteQuery("SelectPaymentBoxById", CommandType.StoredProcedure, parameters);

                paymentBox = dataTable.Rows.Count > 0 ? DbOperation.GetPaymentBox(dataTable.Rows[0]) : null;
            }

            return paymentBox;
        }
    }
}
