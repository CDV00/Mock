using System;

namespace Course.BLL.Requests
{
    public class AudioLanguageForCreateRequest
    {
        public Guid LanguageId { get; set; }
    }
    public class CloseCaptionForCreateRequest : AudioLanguageForCreateRequest { }
}
