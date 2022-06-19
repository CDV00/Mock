using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Models
{
    public class QuizSetting : BaseEntity<Guid>
    {
        [ForeignKey("Quiz")]
        public Guid QuizId { get; set; }
        public bool IsGradable { get; set; } = false;
        public bool IsShowTime { get; set; } = false;
        [Required]
        public long TimeLimit { get; set; }
        [Required]
        public byte PassingScore { get; set; }
        public uint QuestionsLimit { get; set; }
        public Quiz Quiz { get; set; }

    }
}
