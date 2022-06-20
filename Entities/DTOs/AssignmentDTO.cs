using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.DTOs
{
    public class AssignmentDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<AttachmentDTO> Attachments { get; set; }
        public Guid SectionId { get; set; }
        public int Index { get; set; }
    }
}
