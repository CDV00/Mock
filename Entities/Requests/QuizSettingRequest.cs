using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Requests
{
    public class QuizSettingRequest
    {
        public bool IsGradable { get; set; } = false;
        public bool IsShowTime { get; set; } = false;
        public long TimeLimit { get; set; }
        public byte PassingScore { get; set; }
        public uint QuestionsLimit { get; set; }

    }
    public class QuizSettingForCreateRequest
    {
        public bool IsGradable { get; set; } = false;
        public bool IsShowTime { get; set; } = false;
        public long TimeLimit { get; set; }
        public byte PassingScore { get; set; }
        public uint QuestionsLimit { get; set; }

    }
    public class QuizSettingForUpdateRequest
    {
        public Guid Id { get; set; }
        public bool IsGradable { get; set; } = false;
        public bool IsShowTime { get; set; } = false;
        public long TimeLimit { get; set; }
        public byte PassingScore { get; set; }
        public uint QuestionsLimit { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; }

    }

}
