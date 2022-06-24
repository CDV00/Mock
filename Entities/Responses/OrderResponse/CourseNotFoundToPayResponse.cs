namespace Entities.Responses
{
    public class CourseNotFoundToPayResponse : ApiNotFoundResponse
    {
        public CourseNotFoundToPayResponse()
               : base($"Don't have any course to payment")
        {
        }
    }
}
