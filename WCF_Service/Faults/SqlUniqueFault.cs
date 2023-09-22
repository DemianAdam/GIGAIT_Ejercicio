using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Service.Faults
{
    /// <summary>
    /// SqlUniqueFault class. This class represents a fault for a unique constraint violation.
    /// </summary>
    [DataContract]
    public class SqlUniqueFault 
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Details { get; set; }
    }
}
