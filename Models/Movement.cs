using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Models
{
    /// <summary>
    /// Movement model. This model represents a movement of a customer in a payment box.
    /// </summary>
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
