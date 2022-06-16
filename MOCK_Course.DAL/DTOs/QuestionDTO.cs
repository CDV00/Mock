﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.DTOs
{
    public class QuestionDTO
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public byte Score { get; set; }
        public string Type { get; set; }
        public Guid QuizId { get; set; }
        public List<QuizOptionDTO> Options { get; set; }
        public string TextAnswer { get; set; }

    }
}
