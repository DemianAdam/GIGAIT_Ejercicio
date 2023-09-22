using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF_Service.Contracts;

namespace WCF_Service.Services
{
    /// <summary>
    /// DbService class. This class represents the service for the database.
    /// </summary>
    public partial class DbService : IDbService
    {
        public void ResetDatabase()
        {
            using (var context = new DbOperation())
            {
                context.ExecuteNonQuery("ResetDatabase", System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
