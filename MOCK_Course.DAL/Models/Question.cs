using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Models
{
    public class Question : BaseEntity<Guid>
    {
        [Required]
        public string Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public byte Score { get; set; }
        public string Type { get; set; }
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public List<QuizOption> Options { get; set; }
    }
}
