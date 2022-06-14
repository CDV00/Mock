using System;

namespace Course.DAL.Models
{
    public class Attachment : BaseEntity<Guid>
    {
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public string FileUrl { get; set; }
    }
}
