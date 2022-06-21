using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Requests
{
    public class QuizRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public List<QuestionRequest> Questions { get; set; }
        public QuizSettingRequest Settings { get; set; }
        public int Index { get; set; }
    }
    public class QuizForCreateRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<QuestionForCreateRequest> Questions { get; set; }
        public int Index { get; set; }

        public bool IsShowTime { get; set; } = true;
        [Required]
        public long TimeLimit { get; set; } = 999999;
        [Required]
        public byte PassingScore { get; set; } = 100;
        public uint QuestionsLimit { get; set; } = 10;
    }
    public class QuizForUpdateRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<QuestionForUpdateRequest> Questions { get; set; }
        public int Index { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsNew { get; set; } = true;

        public bool IsShowTime { get; set; } = true;
        [Required]
        public long TimeLimit { get; set; } = 999999;
        [Required]
        public byte PassingScore { get; set; } = 100;
        public uint QuestionsLimit { get; set; } = 10;
    }

}
