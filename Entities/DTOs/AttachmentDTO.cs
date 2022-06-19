using Course.DAL.Models;
using System;

namespace Course.DAL.DTOs
{
    public class AttachmentDTO : BaseEntity<Guid>
    {
        public Guid AssignmentId { get; set; }
        public string FileUrl { get; set; }
    }
}
