using Course.BLL.Share.RequestFeatures;
using System;
using System.Collections.Generic;

namespace Entities.ParameterRequest
{
    public class NotificationParameters : RequestParameters
    {
        public string Keyword { get; set; }
        public string Orderby { get; set; }
        //public bool? IsDiscount { get; set; }
    }
}
