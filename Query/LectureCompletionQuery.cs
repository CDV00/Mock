using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Linq;

namespace Query
{
    public class LectureCompletionQuery : QueryBase<LectureCompletion>, ILectureCompletionQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public LectureCompletionQuery(IQueryable<LectureCompletion> lectureCompletionQuery, AppDbContext dbContext) : base(lectureCompletionQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }


        public ILectureCompletionQuery FilterLectureCompletion(Guid userId, Guid sectionId)
        {
            // Query.Include(lc => lc.Lecture).ThenInclude(l => l.Section).Load();
            Query = Query.Where(type => type.UserId == userId && type.Lecture.SectionId == sectionId);
            return this;
        }
        public ILectureCompletionQuery FilterLectureCompletionCourse(Guid userId, Guid courseId)
        {
            Query.Include(lc => lc.Lecture).ThenInclude(l => l.Section).Load();
            Query = Query.Where(lc => lc.UserId == userId && lc.Lecture.Section.CourseId == courseId);
            return this;
        }
    }
}
