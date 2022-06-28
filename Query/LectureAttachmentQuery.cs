using Course.DAL.Data;
using Course.DAL.Models;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Linq;

namespace Course.DAL.Queries
{
    public class LectureAttachmentQuery : QueryBase<LectureAttachment>, ILectureAttachmentQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public LectureAttachmentQuery(IQueryable<LectureAttachment> attachmentQuery, AppDbContext dbContext) : base(attachmentQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }

        public LectureAttachmentQuery FilterById(Guid id)
        {
            Query = Query.Where(la => la.Id == id);
            return this;
        }

    }
}
