using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class SectionCreateRequest
    {
        public string Title { get; set; } = "";

        public IList<LectureForCreateRequest> Lectures { get; set; }
        public IList<AssignmentForCreateRequest> Assignments { get; set; }
        public IList<QuizForCreateRequest> Quizzes { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
    public class SectionUpdateRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public IList<LectureForUpdateRequest> Lectures { get; set; }
        public IList<AssignmentForUpdateRequest> Assignments { get; set; }
        public IList<QuizForUpdateRequest> Quizzes { get; set; }
        public bool IsDeleted { get; set; } = false;
        //public bool IsNew { get; set; } = true;
    }
}
