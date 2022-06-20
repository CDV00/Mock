using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.DTOs
{
    public class QuizOptionDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCorrectAnswer { get; set; } = false;
        public Guid QuestionId { get; set; }
    }
}
