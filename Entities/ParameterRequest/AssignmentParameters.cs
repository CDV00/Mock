using Course.BLL.Share.RequestFeatures;

namespace Entities.ParameterRequest
{
    public class QuizParameters : RequestParameters
    {
        public string Keyword { get; set; }
        public string Orderby { get; set; }
    }
}
