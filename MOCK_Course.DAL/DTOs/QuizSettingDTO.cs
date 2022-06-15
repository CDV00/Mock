using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.DTOs
{
    public class QuizSettingDTO
    {
        public Guid QuizId { get; set; }
        public bool IsGradable { get; set; } = false;
        public bool IsShowTime { get; set; } = false;
        public long TimeLimit { get; set; }
        public byte PassingScore { get; set; }
        public uint QuestionsLimit { get; set; }
    }
}
