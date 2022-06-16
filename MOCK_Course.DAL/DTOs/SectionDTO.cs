using Course.DAL.DTOs;
using System;
using System.Collections.Generic;

namespace Course.BLL.Responses
{
    public class SectionDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int TotalTime { get; set; } = 0;

        public IList<LectureDTO> Lectures { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; } = false;
        public IList<AssignmentDTO> Assignments { set; get; }
        public IList<QuizDTO> Quizzes { set; get; }
    }


    public class LectureDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string VideoExternalUrl { get; set; }
        public string VideoPoster { get; set; }
        public bool IsPreview { get; set; }
        public int TotalTime { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; } = false;
    }

}
