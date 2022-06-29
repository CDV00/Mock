using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Course.DAL.Models
{
    public class Quiz : BaseEntity<Guid>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [ForeignKey("Section")]
        public Guid SectionId { get; set; }
        public Section Section { get; set; }
        public int Index { set; get; }
        public IList<Question> Questions { get; set; }

        public bool IsShowTime { get; set; } = true;
        [Required]
        public long TimeLimit { get; set; } = 999999;
        [Required]
        public byte PassingScore { get; set; } = 100;
        public uint QuestionsLimit { get; set; } = 10;

        public IList<QuizCompletion> QuizCompletion { get; set; }
    }
}
