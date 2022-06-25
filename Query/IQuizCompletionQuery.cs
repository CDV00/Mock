﻿using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Query
{
    public interface IQuizCompletionQuery : IQuery<QuizCompletion>
    {
        QuizCompletionQuery FilterByQuiz(Guid quizId);
        QuizCompletionQuery FilterByUser(Guid userId);
    }
}