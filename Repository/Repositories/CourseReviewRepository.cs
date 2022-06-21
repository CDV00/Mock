using AutoMapper;
using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Entities.ParameterRequest;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CourseReviewRepository : Repository<CourseReview>, ICourseReviewRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public CourseReviewRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICourseReviewQuery BuildQuery()
        {
            return new CourseReviewQuery(_context.CourseReviews.AsQueryable(), _context);
        }
        //public async Task<int> GetTotal(Guid userId)
        //{
        //    return await GetAll().Where(s => s.Enrollment.UserId == userId).GroupBy(s => s.Enrollment.UserId).CountAsync();
        //}
        public async Task<PagedList<CourseReviewDTO>> GetAllCourseReview(Guid courseId, CourseReviewParameters parameter)
        {
            var courseReview = await BuildQuery()
                                     .FilterByCourseId(courseId)
                                     .FilterByKeyword(parameter.Keyword)
                                     .IncludeUser()
                                     .ApplySort(parameter.Orderby)
                                     .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                                     .Take(parameter.PageSize)
                                     .ToListAsync(c => _mapper.Map<CourseReviewDTO>(c));

            var count = await BuildQuery()
                              .FilterByCourseId(courseId)
                              .FilterByKeyword(parameter.Keyword)
                              .CountAsync();

            return new PagedList<CourseReviewDTO>(courseReview, count, parameter.PageNumber, parameter.PageSize);
        }

    }
}
