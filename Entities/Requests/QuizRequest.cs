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
        public QuizSettingForCreateRequest Settings { get; set; }
        public int Index { get; set; }
    }
    public class QuizForUpdateRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<QuestionForUpdateRequest> Questions { get; set; }
        public QuizSettingForUpdateRequest Settings { get; set; }
        public int Index { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; }
    }

}
