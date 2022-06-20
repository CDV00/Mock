using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Repositories.Abstraction;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CourseRepository : Repository<Courses>, ICourseRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CourseRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICourseQuery BuildQuery()
        {
            return new CourseQuery(_context.Courses.AsQueryable(), _context);
        }

        public async Task<bool> IsExist(Guid id)
        {
            return await BuildQuery().FilterById(id)
                                     .AnyAsync();
        }

        public async Task<CourseDTO> GetDetailCourseAsync(Guid id)
        {
            var course = await BuildQuery().IncludeCategory()
                                           .IncludeLanguage()
                                           .IncludeLevel()
                                           .IncludeSection()
                                           .IncludeQuiz()
                                           .IncludeAssignment()
                                           .IncludeUser()
                                           .FilterById(id)
                                           .AsSelectorAsync(x => _mapper.Map<CourseDTO>(x));

            return _mapper.Map<CourseDTO>(course);
        }

        public async Task<PagedList<CourseDTO>> GetAllCourseAsync(CourseParameters parameters)
        {
            var courses = await BuildQuery().IncludeCategory()
                                            .IncludeUser()
                                            .IncludeDiscount()
                                            .FilterByKeyword(parameters.Keyword)
                                            .FilterByUserId(parameters.userId)
                                            .FilterByCategoryId(parameters.CategoryId)
                                            .FilterByAudioLanguageIds(parameters.AudioLanguageIds)
                                            .FilterByCloseCaptionIds(parameters.CloseCaptionIds)
                                            .FilterByLevelIds(parameters.LevelIds)
                                            .FilterByPrice(parameters.IsFree, parameters.IsDiscount, parameters.MinPrice, parameters.MaxPrice)
                                            .FilterByRating(parameters.Rate)
                                            .ApplySort(parameters.Orderby)
                                            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                            .Take(parameters.PageSize)
                                            .ToListAsync(c => _mapper.Map<CourseDTO>(c));

            var count = await BuildQuery().FilterByKeyword(parameters.Keyword)
                                          .FilterByUserId(parameters.userId)
                                          .FilterByCategoryId(parameters.CategoryId)
                                          .FilterByAudioLanguageIds(parameters.AudioLanguageIds)
                                          .FilterByCloseCaptionIds(parameters.CloseCaptionIds)
                                          .FilterByLevelIds(parameters.LevelIds)
                                          .FilterByPrice(parameters.IsFree, parameters.IsDiscount, parameters.MinPrice, parameters.MaxPrice)
                                          .FilterByRating(parameters.Rate)
                                          .CountAsync();

            return new PagedList<CourseDTO>(courses, count, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<CourseDTO>> GetAllMyCoures(Guid? userId, CourseParameters parameters)
        {
            var courses = await BuildQuery().IncludeCategory()
                                            .IncludeUser()
                                            .IncludeDiscount()
                                            .FilterByUserId(userId)
                                            .FilterByApprove()
                                            .IncludeCategory()
                                            .FilterByKeyword(parameters.Keyword)
                                            .FilterByUserId(parameters.userId)
                                            .FilterByCategoryId(parameters.CategoryId)
                                            .FilterByAudioLanguageIds(parameters.AudioLanguageIds)
                                            .FilterByCloseCaptionIds(parameters.CloseCaptionIds)
                                            .FilterByLevelIds(parameters.LevelIds)
                                            .FilterByPrice(parameters.IsFree, parameters.IsDiscount, parameters.MinPrice, parameters.MaxPrice)
                                            .FilterByRating(parameters.Rate)
                                            .ApplySort(parameters.Orderby)
                                            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                            .Take(parameters.PageSize)
                                            .ToListAsync(c => _mapper.Map<CourseDTO>(c));

            var count = await BuildQuery().FilterByKeyword(parameters.Keyword)
                                          .FilterByUserId(parameters.userId)
                                          .FilterByCategoryId(parameters.CategoryId)
                                          .FilterByAudioLanguageIds(parameters.AudioLanguageIds)
                                          .FilterByCloseCaptionIds(parameters.CloseCaptionIds)
                                          .FilterByLevelIds(parameters.LevelIds)
                                          .FilterByPrice(parameters.IsFree, parameters.IsDiscount, parameters.MinPrice, parameters.MaxPrice)
                                          .FilterByRating(parameters.Rate)
                                          .CountAsync();

            return new PagedList<CourseDTO>(courses, count, parameters.PageNumber, parameters.PageSize);
        }
        public async Task<PagedList<CourseDTO>> UpcomingCourse(Guid? userId, CourseParameters parameters)
        {
            Status status = Status.Review;
            var courses = await BuildQuery().IncludeCategory()
                                            //.IncludeUser()
                                            //.IncludeDiscount()
                                            .FilterByUserId(userId)
                                            .FilterStatus(status)
                                            .FilterByKeyword(parameters.Keyword)
                                            .FilterByUserId(parameters.userId)
                                            .FilterByCategoryId(parameters.CategoryId)
                                            .FilterByAudioLanguageIds(parameters.AudioLanguageIds)
                                            .FilterByCloseCaptionIds(parameters.CloseCaptionIds)
                                            .FilterByLevelIds(parameters.LevelIds)
                                            .FilterByPrice(parameters.IsFree, parameters.IsDiscount, parameters.MinPrice, parameters.MaxPrice)
                                            .FilterByRating(parameters.Rate)
                                            .ApplySort(parameters.Orderby)
                                            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                            .Take(parameters.PageSize)
                                            .ToListAsync(c => _mapper.Map<CourseDTO>(c));

            var count = await BuildQuery().FilterByKeyword(parameters.Keyword)
                                          .FilterByUserId(parameters.userId)
                                          .FilterByCategoryId(parameters.CategoryId)
                                          .FilterByAudioLanguageIds(parameters.AudioLanguageIds)
                                          .FilterByCloseCaptionIds(parameters.CloseCaptionIds)
                                          .FilterByLevelIds(parameters.LevelIds)
                                          .FilterByPrice(parameters.IsFree, parameters.IsDiscount, parameters.MinPrice, parameters.MaxPrice)
                                          .FilterByRating(parameters.Rate)
                                          .CountAsync();

            return new PagedList<CourseDTO>(courses, count, parameters.PageNumber, parameters.PageSize);
        }
        public async Task<PagedList<CourseDTO>> GetAllMyPurchase(Guid userId, CourseParameters parameters)
        {
            Status status = Status.Review;
            var courses = await BuildQuery().IncludeCategory()
                                            .IncludeUser()
                                            .IncludeDiscount()
                                            .FilterByOrderd(userId)
                                            .FilterStatus(status)
                                            .FilterByKeyword(parameters.Keyword)
                                            .FilterByUserId(parameters.userId)
                                            .FilterByCategoryId(parameters.CategoryId)
                                            .FilterByAudioLanguageIds(parameters.AudioLanguageIds)
                                            .FilterByCloseCaptionIds(parameters.CloseCaptionIds)
                                            .FilterByLevelIds(parameters.LevelIds)
                                            .FilterByPrice(parameters.IsFree, parameters.IsDiscount, parameters.MinPrice, parameters.MaxPrice)
                                            .FilterByRating(parameters.Rate)
                                            .ApplySort(parameters.Orderby)
                                            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                            .Take(parameters.PageSize)
                                            .ToListAsync(c => _mapper.Map<CourseDTO>(c));

            var count = await BuildQuery().FilterByKeyword(parameters.Keyword)
                                          .FilterByUserId(parameters.userId)
                                          .FilterByCategoryId(parameters.CategoryId)
                                          .FilterByAudioLanguageIds(parameters.AudioLanguageIds)
                                          .FilterByCloseCaptionIds(parameters.CloseCaptionIds)
                                          .FilterByLevelIds(parameters.LevelIds)
                                          .FilterByPrice(parameters.IsFree, parameters.IsDiscount, parameters.MinPrice, parameters.MaxPrice)
                                          .FilterByRating(parameters.Rate)
                                          .CountAsync();

            return new PagedList<CourseDTO>(courses, count, parameters.PageNumber, parameters.PageSize);
        }
    }
}
