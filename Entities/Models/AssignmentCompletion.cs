using System;

namespace Course.DAL.Models
{
    public class AssignmentCompletion
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}
