using System;

namespace Entities.Responses
{
    public class NotMathAudioLanguageResponse : ApiNotFoundResponse
    {
        public NotMathAudioLanguageResponse(string ids)
        : base($"audio languages ids:{ids} not math in db.")
        {
        }
    }
}
