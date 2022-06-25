﻿using Entities.DTOs;
using System;
using System.Collections.Generic;
namespace Course.DAL.DTOs
{
    public class QuizDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<QuestionDTO> Questions { get; set; }
        public int Index { get; set; }

        public bool IsDeleted { get; set; } = false;
        public bool IsNew { get; set; } = false;
        //[JsonIgnore]
        //public QuizSettingDTO Settings { get; set; }

        public bool IsShowTime { get; set; }
        public long TimeLimit { get; set; }
        public byte PassingScore { get; set; }
        public uint QuestionsLimit { get; set; }
        public ICollection<QuizCompletionDTO> QuizCompletions { get; set; }
    }
}
