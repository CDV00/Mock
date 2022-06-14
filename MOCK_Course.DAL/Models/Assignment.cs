using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Models
{
    public class Assignment : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<Attachment> Attachments { get; set; }

        public Guid SectionId { get; set; }
        public Section Section { get; set; }
        public int Index { get; set; }
    }
}
