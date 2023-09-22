using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// PaymentBox model. This model represents a payment box.
    /// </summary>
    [DataContract]
    public class PaymentBox
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
    }
}
