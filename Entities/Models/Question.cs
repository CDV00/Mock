﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.DAL.Models
{
    public class Question : BaseEntity<Guid>
    {
        [Required]
        public string Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public byte Score { get; set; }
        public TypeQuestion Type { get; set; }
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public IList<QuizOption> Options { get; set; }
        public string TextAnswer { get; set; }
    }
    public enum TypeQuestion
    {
        SingleChoice,
        MultipleChoice,
        SingleLineText,
        MiltiText
    }
}
