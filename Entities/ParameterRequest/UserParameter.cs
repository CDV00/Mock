using Course.BLL.Share.RequestFeatures;

namespace Entities.ParameterRequest
{
    public class UserParameter : RequestParameters
    {
        public string Keyword { get; set; }
        public bool IsPopular { get; set; } = true;
    }
}
