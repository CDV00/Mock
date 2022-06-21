using System;

namespace Entities.Responses
{
    public class CourseReviewNotFound : ApiNotFoundResponse
    {
        public CourseReviewNotFound(Guid id)
        : base($"Don't have this reviews")
        {
        }
    }
}
