﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.BLL.Requests
{
    public class SectionCreateRequest
    {
        public string Title { get; set; }

        public IList<LectureForCreateRequest> Lectures { get; set; }
        public IList<AssignmentForCreateRequest> Assignments { get; set; }
        public IList<QuizForCreateRequest> Quizzes { get; set; }
    }
    public class SectionUpdateRequest
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public IList<LectureForUpdateRequest> Lectures { get; set; }
        public IList<AssignmentForUpdateRequest> Assignments { get; set; }
        public IList<QuizForUpdateRequest> Quizzes { get; set; }
        //them attachment, quiz (bỏ id) isactive
        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; }
    }
}