using System;

namespace Course.DAL.Models
{
    public class Attachment : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public string FileUrl { get; set; }
    }
}
