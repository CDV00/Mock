using Course.DAL.Models;
using System;

namespace Course.DAL.DTOs
{
    public class AttachmentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AssignmentId { get; set; }
        public string FileUrl { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
