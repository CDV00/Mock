using System;

namespace Course.BLL.Requests
{
    public class AudioLanguageForCreateRequest
    {
        public Guid Id { get; set; }
    }
    public class CloseCaptionForCreateRequest : AudioLanguageForCreateRequest { }

    public class CourseLevelForCreateRequest
    {
        public Guid Id { get; set; }
    }
}
