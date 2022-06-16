using System;

namespace CourseAPI.ErrorModel
{
    public abstract class NotFoundException : Exception
    {
        public NotFoundException(string message)
        : base(message)
        { }
    }
}
