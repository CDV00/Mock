using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.DAL.Models
{
    public class Lecture : BaseEntity<Guid>
    {
        public string Title { get; set; }
        [MaxLength]
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public string VideoExternalUrl { get; set; }
        public string VideoPoster { get; set; }
        //public string AttachmentUrl { get; set; }
        public ICollection<LectureAttachment> Attachments { get; set; }
        public int Index { get; set; }
        public bool IsPreview { get; set; }
        public int TotalTime { get; set; }

        public Guid SectionId { get; set; }
        public Section Section { get; set; }

        public IList<LectureCompletion> LectureCompletion { get; set; }
    }
}
