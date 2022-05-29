using System;

namespace Course.BLL.Requests
{
    public class AudioLanguageForCreateRequest
    {
        //public Guid courseId { get; set; }
        public Guid LanguageId { get; set; }
    }
    public class CloseCaptionForCreateRequest : AudioLanguageForCreateRequest { }
    public class LanguageResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
