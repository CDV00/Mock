using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Entities.ParameterRequest;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SavedCoursesRepository : Repository<SavedCourses>, ISavedCoursesRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public SavedCoursesRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public ISavedCoursesQuery BuildQuery()
        {
            return new SavedCoursesQuery(_context.SavedCourses.AsQueryable(), _context);
        }
        public async Task<PagedList<SavedCoursesDTO>> GetAllSavedCourses(Guid userId, SavedCoursesParameters savedCoursesParameters)
        {
            var savedCourses = await BuildQuery().FilterByUserId(userId)
                                                 .IncludeCourse()
                                                 .IncludeUser()
                                                 .IncludeCategory()
                                                  .IncludeDiscount()
                                                  .Skip((savedCoursesParameters.PageNumber - 1) * savedCoursesParameters.PageSize)
                                                  .Take(savedCoursesParameters.PageSize)
                                                  .ToListAsync(c => _mapper.Map<SavedCoursesDTO>(c));

            var count = await BuildQuery().FilterByUserId(userId)
                                            .CountAsync();

            return new PagedList<SavedCoursesDTO>(savedCourses, count, savedCoursesParameters.PageNumber, savedCoursesParameters.PageSize);
        }
        public async Task<bool> CheckExistSaveCourse(Guid courseId, Guid userId)
        {
            return await BuildQuery().FilterByUserId(userId)
                                     .FilterByCourseId(courseId)
                                     .AnyAsync();
        }
    }
}
