using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Service.Faults
{
    /// <summary>
    /// SqlInvalidOperationException class. This class represents a SqlInvalidOperation exception.
    /// </summary>
    [DataContract]
    public class SqlInvalidOperationException
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Details { get; set; }
    }
}
