using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        //[JsonIgnore]
        //public QuizSettingDTO Settings { get; set; }

    }
}
