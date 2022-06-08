

using Course.BLL.Share.RequestFeatures;
using System;
using System.Collections.Generic;

namespace Course.BLL.Requests
{
    public class CourseParameters : RequestParameters
    {
        public string Keyword { get; set; }
        public Guid? CategoryId { get; set; }
        public List<Guid?> AudioLanguageIds { get; set; }
        public List<Guid?> CloseCaptionIds { get; set; }
        public List<Guid?> LevelIds { get; set; }
        public string Orderby { get; set; }
        public bool? IsSeller { get; set; }
    }
}
