using System;
using System.Collections.Generic;

namespace Course.DAL.Models
{
    public class Assignment : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IList<Attachment> Attachments { get; set; }

        public Guid SectionId { get; set; }
        public Section Section { get; set; }
        public int Index { get; set; }
        public ICollection<AssignmentCompletion> AssignmentCompletion { get; set; }
    }
}
