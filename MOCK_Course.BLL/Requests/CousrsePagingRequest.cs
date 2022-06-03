namespace Course.BLL.Requests
{
    public class CousrsePagingRequest
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public string sortBy { get; set; }
    }
}
