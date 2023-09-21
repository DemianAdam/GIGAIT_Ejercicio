using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WCF_Service.Contracts;
using Models;

namespace WCF_Service.Services
{

    public partial class MovementService : IMovementService
    {
        //  [OperationContract(Name = "AddMovement")]
        public int Add(Movement movement)
        {
            using (var context = new DbOperation())
            {

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@payment_box_id", movement.PaymentBox is null? Convert.DBNull : movement.PaymentBox.Id),
                    new SqlParameter("@customer_id", movement.Customer is null ? Convert.DBNull : movement.Customer.Id),
                    new SqlParameter("@creation_date", movement.CreationDate),
                    new SqlParameter("@ocurred_date",movement.OcurredDate ?? Convert.DBNull)
                };

                return Convert.ToInt32(context.ExecuteQueryScalar("AddMovement", CommandType.StoredProcedure, parameters));
            }
        }

        public void Delete(Movement entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Movement movement)
        {
            using (var context = new DbOperation())
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@movement_id", movement.Id),
                    new SqlParameter("@customer_id", movement.Customer.Id),
                };

                context.ExecuteNonQuery("UpdateMovement", CommandType.StoredProcedure, parameters);
            }
        }

        IEnumerable<Movement> ICRUDService<Movement>.SelectAll()
        {
            List<Movement> movements = new List<Movement>();

            using (var context = new DbOperation())
            {
                var dataTable = context.ExecuteQuery("SelectMovements", CommandType.StoredProcedure);

                foreach (DataRow row in dataTable.Rows)
                {
                    Movement movement = DbOperation.GetMovement(row);
                    movements.Add(movement);
                }
            }
            return movements;
        }

        // [OperationContract(Name = "SelectMovementByID")]
        Movement ICRUDService<Movement>.SelectById(int id)
        {
            Movement movement = new Movement();
            using (DbOperation context = new DbOperation())
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", id)
                };

                var dataTable = context.ExecuteQuery("SelectMovementByID", CommandType.StoredProcedure, parameters);

                movement = dataTable.Rows.Count > 0 ? DbOperation.GetMovement(dataTable.Rows[0]) : null;
            }

            return movement;
        }
    }
}
