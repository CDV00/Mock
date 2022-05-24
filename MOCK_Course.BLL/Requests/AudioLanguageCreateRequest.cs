using System;

namespace Course.BLL.Requests
{
    public class AudioLanguageCreateRequest
    {
        public Guid LanguageId { get; set; }
    }
    public class CloseCaptionCreateRequest : AudioLanguageCreateRequest { }
    public class LanguageResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
