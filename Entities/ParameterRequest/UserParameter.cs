using Course.BLL.Share.RequestFeatures;

namespace Entities.ParameterRequest
{
    public class UserParameter : RequestParameters
    {
        public string Keyword { get; set; }
        public string Role { get; set; }
        public string Orderby { get; set; }
        public bool IsPopular { get; set; } = true;
    }
}
