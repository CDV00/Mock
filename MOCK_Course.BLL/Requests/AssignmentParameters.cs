

using Course.BLL.Share.RequestFeatures;
using System;
using System.Collections.Generic;

namespace Course.BLL.Requests
{
    public class QuizParameters : RequestParameters
    {
        public string Keyword { get; set; }
        public string Orderby { get; set; }
        //public bool? IsDiscount { get; set; }
    }
}
