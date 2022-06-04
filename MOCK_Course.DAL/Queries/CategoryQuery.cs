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
    public class CategoryQuery : QueryBase<Category>, ICategoryQuery
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="masterDataQuery"></param>
        /// <param name="dbContext"></param>
        public CategoryQuery(IQueryable<Category> courseQuery, AppDbContext dbContext) : base(courseQuery)
        { _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); }



        /// <summary>
        /// FilterIsActive
        /// </summary>
        /// <param name="isActice"></param>
        /// <returns></returns>
        public ICategoryQuery FilterIsActive(bool? isActice)
        {
            Query = Query.Where(type => isActice == null || type.IsActive == isActice);
            return this;
        }

        public ICategoryQuery FilterByParent(Guid? id)
        {
            Query = Query.Where(type => type.ParentId == id);
            return this;
        }

        public ICategoryQuery IncludeSubCategory()
        {
            Query.Include(c => c.SubCategories).Load();
            return this;
        }
    }
}
