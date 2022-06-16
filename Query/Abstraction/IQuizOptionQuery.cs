using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.DAL.Queries.Abstraction
{
    public interface IQuizOptionQuery : IQuery<QuizOption>
    {
    }
}