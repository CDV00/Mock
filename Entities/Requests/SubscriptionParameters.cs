using Course.BLL.Share.RequestFeatures;
using System;
using System.Collections.Generic;

namespace Course.BLL.Requests
{
    public class SubscriptionParameters : RequestParameters
    {
        public string Keyword { get; set; }
    }
}
