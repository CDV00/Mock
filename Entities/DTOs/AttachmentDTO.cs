using Course.DAL.Models;
using System;

namespace Course.DAL.DTOs
{
    public class AttachmentDTO
    {
        public Guid Id { get; set; }
        public Guid AssignmentId { get; set; }
        public string FileUrl { get; set; }

        public bool IsDeleted { get; set; } = false;
        public bool IsNew { get; set; } = false;
    }
}
