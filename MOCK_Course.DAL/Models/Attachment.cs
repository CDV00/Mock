using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Models
{
    public class Attachment : BaseEntity<Guid>
    {
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public string FileUrl { get; set; }
    }
}
