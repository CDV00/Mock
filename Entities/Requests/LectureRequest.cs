﻿using System;
using System.Collections.Generic;

namespace Course.BLL.Requests
{
    public class LectureForCreateRequest
    {
        //public Guid? SectionId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string VideoUrl { get; set; } = "";
        public string VideoExternalUrl { get; set; } = "";
        public string VideoPoster { get; set; } = "";
        public string AttachmentUrl { get; set; } = "";
        public int Index { get; set; } = 0;
        public bool IsPreview { get; set; } = false;
        public int TotalTime { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public IList<LectureAttachmentForCreateRequest> Attachments { get; set; }
    }

    public class LectureAttachmentForCreateRequest
    {
        public string Name { get; set; } = "";
        public string FileUrl { get; set; } = "";
    }

    public class LectureAttachmentForUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string FileUrl { get; set; } = "";
        public bool IsDeleted { get; set; } = false;
    }


    public class LectureForUpdateRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string VideoUrl { get; set; } = "";
        public string VideoExternalUrl { get; set; } = "";
        public string VideoPoster { get; set; } = "";
        public string AttachmentUrl { get; set; } = "";
        public int Index { get; set; } = 0;
        public bool IsPreview { get; set; } = false;
        public int TotalTime { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public IList<LectureAttachmentForUpdateRequest> Attachments { get; set; }
    }
}
