using System;

namespace Entities.DTOs
{
    public class QuizCompletionDTO
    {
        public Guid UserId { get; set; }
        public Guid QuizId { get; set; }
    }
}
