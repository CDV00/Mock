using System;

namespace Entities.Responses
{
    public class NotMathIdResponse : ApiNotFoundResponse
    {
        public NotMathIdResponse(string name, string ids)
        : base($"{name} ids:{ids} not math in db.")
        {
        }
    }
}
