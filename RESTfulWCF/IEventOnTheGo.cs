using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RESTfulWCF
{
    [ServiceContract]
    public interface IEventOnTheGo
    {
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "checkPassword/{password}/forUsername/{username}")]
        User PasswordCheck(string password, string username);
    }
}
