using Course.DAL.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Course.BLL.Responses
{
    public class SectionDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int TotalTime { get; set; } = 0;

        public IList<LectureDTO> Lectures { get; set; }
        public IList<AssignmentDTO> Assignments { set; get; }
        public IList<QuizDTO> Quizzes { set; get; }
        public bool IsDeleted { get; set; } = false;
    }


    public class LectureDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string VideoExternalUrl { get; set; }
        public string VideoPoster { get; set; }
        public string AttachmentUrl { get; set; }
        public int Index { get; set; }
        public bool IsPreview { get; set; }
        public int TotalTime { get; set; }
        public bool isCompleted { get; set; } = false;

        public LectureCompletionDTO LectureCompletion { get; set; }

        public bool IsDeleted { get; set; } = false;
        public IList<LectureAttachmentDTO> Attachments { get; set; }
    }

    public class LectureAttachmentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string FileUrl { get; set; } = "";
        public bool IsDeleted { get; set; } = false;
    }

}
