using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RESTfulWCF
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public bool result { get; set; }
    }
}