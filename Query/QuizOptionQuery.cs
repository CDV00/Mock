using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Queries
{
    public class QuizOptionQuery : QueryBase<QuizOption>, IQuizOptionQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public QuizOptionQuery(IQueryable<QuizOption> quizOptionQuery, AppDbContext dbContext) : base(quizOptionQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }


    }
}
