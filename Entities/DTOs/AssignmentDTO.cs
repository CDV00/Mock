﻿using Entities.DTOs;
using System;
using System.Collections.Generic;

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
        public bool Completion { get; set; } = false;

        public bool IsDeleted { get; set; } = false;
        public AssignmentCompletionDTO assignmentCompletion { get; set; }
        //public bool IsNew { get; set; } = false;
    }
}
