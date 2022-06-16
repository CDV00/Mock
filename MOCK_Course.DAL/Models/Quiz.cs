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
        public List<Question> Questions { get; set; }
        public List<QuizSetting> Settings { get; set; }
    }
}
