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
        public QuizSettingDTO Settings { get; set; }
        public int Index { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; } = false;
        //[JsonIgnore]
        //public QuizSettingDTO Settings { get; set; }

    }
}
