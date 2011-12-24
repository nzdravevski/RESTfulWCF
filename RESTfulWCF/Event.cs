using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RESTfulWCF
{
    [DataContract]
    public class Event
    {
        [DataMember]
        public string userId { get; set; }

        [DataMember]
        public string venueName { get; set; }

        [DataMember]
        public double venueLat { get; set; }

        [DataMember]
        public double venueLon { get; set; }

        [DataMember]
        public string eventName { get; set; }

        [DataMember]
        public string eventGoal { get; set; }

        [DataMember]
        public DateTime dateFrom { get; set; }

        [DataMember]
        public DateTime dateTo { get; set; }
    }
}