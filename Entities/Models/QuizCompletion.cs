using System;

namespace Course.DAL.Models
{
    public class QuizCompletion
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
