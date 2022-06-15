using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Models
{
    public class QuizOption : BaseEntity<Guid>
    {
        [Required]
        public string Title { get; set; }
        public bool IsCorrectAnswer { get; set; } = false;
        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }

}
