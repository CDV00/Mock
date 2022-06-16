using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Linq;

namespace Course.DAL.Queries
{
    public class LectureQuery : QueryBase<Lecture>, ILectureQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public LectureQuery(IQueryable<Lecture> courseQuery, AppDbContext dbContext) : base(courseQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }


        public ILectureQuery FilterBySectionId(Guid sectionId)
        {
            Query = Query.Where(type => type.SectionId == sectionId);
            return this;
        }
        public ILectureQuery FilterLecturebyCourse(Guid courseId)
        {
            //Query.Include(l => l.Section).Load();
            Query = Query.Where(l => l.Section.CourseId == courseId);
            return this;
        }
    }
}
