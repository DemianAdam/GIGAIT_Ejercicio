using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Models
{
    [DataContract]
    public class Movement
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public PaymentBox PaymentBox { get; set; }
        [DataMember]
        public DateTime CreationDate { get; set; }
        [DataMember]
        public DateTime? OcurredDate { get; set; }

    }
}
