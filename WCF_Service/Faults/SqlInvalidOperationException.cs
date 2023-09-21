using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Service.Faults
{
    [DataContract]
    public class SqlInvalidOperationException
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Details { get; set; }
    }
}
