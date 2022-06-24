using Course.BLL.Share.RequestFeatures;

namespace Entities.ParameterRequest
{
    public class CourseReviewParameters : RequestParameters
    {
        public string Keyword { get; set; }
        public string Orderby { get; set; }
        public float? Rating { get; set; }
    }
}
