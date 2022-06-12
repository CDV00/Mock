using Course.BLL.Share.RequestFeatures;

namespace Course.BLL.Requests
{
    public class UserParameter : RequestParameters
    {
        public string Keyword { get; set; }
        public bool IsPopular { get; set; } = true;
    }
}
