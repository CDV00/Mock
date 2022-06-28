using Course.DAL.Data;
using Entities.Models;
using Query.Abstraction;
using SES.HomeServices.Data.Queries.Abstractions;
using System;
using System.Linq;

namespace Course.DAL.Queries
{
    public class DepositQuery : QueryBase<Deposit>, IDepositQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public DepositQuery(IQueryable<Deposit> courseQuery, AppDbContext dbContext) : base(courseQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }

        public IDepositQuery FilterByUser(Guid userId)
        {
            Query = Query.Where(d => d.UserId == userId);
            return this;
        }
        public IDepositQuery FilterDateStart(DateTime? dateStart)
        {
            if (dateStart == null)
                return this;
            Query = Query.Where(d => d.CreatedAt.Date >= dateStart.Value.Date);
            return this;
        }
        public IDepositQuery FilterDateEnd(DateTime? dateSend)
        {
            if (dateSend == null)
                return this;
            Query = Query.Where(d => d.CreatedAt.Date >= dateSend.Value.Date);
            return this;
        }
    }
}
