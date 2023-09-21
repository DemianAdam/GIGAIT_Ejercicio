using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF_Service.Contracts;

namespace WCF_Service.Services
{
    //GIGAIT_Service
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DbService" in both code and config file together.
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
