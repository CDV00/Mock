﻿using Course.BLL.Share.RequestFeatures;
using System;
using System.Collections.Generic;

namespace Entities.ParameterRequest
{
    public class CourseReviewParameters : RequestParameters
    {
        public string Keyword { get; set; }
        public string Orderby { get; set; }
        public float? Rating { get; set; }
        //public bool? IsDiscount { get; set; }
    }
}
