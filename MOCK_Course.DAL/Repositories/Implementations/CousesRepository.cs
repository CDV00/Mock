using AutoMapper;
using Course.BLL.Responses;
using Course.DAL.Data;
using Course.DAL.DTOs;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Implementations
{
    public class CousesRepository : Repository<Courses, Guid>, ICousesRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public CousesRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public override Task CreateAsync(Courses _object)
        {

            return base.CreateAsync(_object);
        }

        public async Task<Courses> GetForUpdate(Guid id)
        {
            var course = await GetAll().Where(c => c.Id == id).FirstOrDefaultAsync();
            //_context.Entry(course).State = EntityState.Modified;

            return course;
        }

        public async Task<IEnumerable<Courses>> GetAllForCard()
        {

            return await GetAll().Include(c => c.Category).Include(c => c.User).ToListAsync();
        }

        public async Task<CourseForDetailDTO> GetDetail(Guid id)
        {
            var courseDetail = Entity().Include(c => c.User).Include(c => c.AudioLanguages).Include(c => c.CloseCaptions).Include(c => c.Category).Include(c => c.Sections).ThenInclude(s => s.Lectures).Where(c => c.Id == id).Select(c => _mapper.Map<CourseForDetailDTO>(c)).FirstOrDefault();
            //var courseDetail = Entity().Where(c => c.Id == id).Select(c => new CourseForDetailDTO() {Title = c.ca }).FirstOrDefault();
            return courseDetail;
        }

        public async Task<Courses> GetForPost(Guid id)
        {
            return await FindByCondition(c => c.Id == id).Include(c => c.Category).Include(c => c.AudioLanguages).Include(c => c.CloseCaptions).Include(c => c.Sections).ThenInclude(s => s.Lectures).Include(c => c.Levels).FirstOrDefaultAsync();
        }
        public async Task<int> GetTotal(Guid userId)
        {
            return await GetAll().Where(s => s.UserId == userId).GroupBy(s => s.UserId).CountAsync();
        }

        public async Task<List<PurchaseDTO>> GetAllMyPurchase(Guid userId)
        {
            var purchases = await (from order in _context.Orders
                                   join course in _context.Courses on order.CourseId equals course.Id
                                   join user in _context.Users on course.UserId equals user.Id
                                   join category in _context.Categories on course.CategoryId equals category.Id
                                   where (order.UserId == userId)
                                   select new PurchaseDTO
                                   {
                                       FullName = user.Fullname,
                                       Title = course.Title,
                                       Category = new BLL.DTO.CourseCategoryDTO() { Name = category.Name, Id = category.Id },
                                       Price = order.Price,
                                       CreatedAt = order.CreatedAt
                                   }).ToListAsync();

            return purchases;
        }
        public async Task<List<MyCoursesDTO>> GetAllMyCoures(Guid userId)
        {
            var course = await (from courses in _context.Courses
                                join category in _context.Categories on courses.CategoryId equals category.Id
                                let salecount =
                                (
                                  from order in _context.Orders
                                  where courses.Id == order.CourseId
                                  select order
                                ).Count()
                                let partscount =
                                (
                                  from enrollment in _context.Enrollment
                                  where courses.Id == enrollment.CourseId
                                  select enrollment
                                ).Count()
                                where (courses.UserId == userId && courses.IsActive == true)

                                select new MyCoursesDTO
                                {
                                    Id = courses.Id,
                                    Title = courses.Title,
                                    CreatedAt = courses.CreatedAt,
                                    Category = new BLL.DTO.CourseCategoryDTO() { Name = category.Name, Id = category.Id },
                                    Sale = salecount,
                                    Parts = partscount,
                                    Status = courses.IsActive
                                }).ToListAsync();

            return course;
        }
        public async Task<List<UpcommingCourseDTO>> GetAllUpcomingCourses(Guid userId)
        {
            var upcommingcourse = await (
                                from courses in _context.Courses
                                join category in _context.Categories on courses.CategoryId equals category.Id
                                where (courses.UserId == userId && courses.IsActive == false)
                                select new UpcommingCourseDTO
                                {
                                    Id = courses.Id,
                                    Title = courses.Title,
                                    Thumbnail = courses.ThumbnailUrl,
                                    Category = new BLL.DTO.CourseCategoryDTO() { Name = category.Name, Id = category.Id },
                                    Price = courses.Price,
                                    CreatedAt = courses.CreatedAt,
                                    Status = courses.IsActive
                                }).ToListAsync();

            return upcommingcourse;
        }

        public async Task<List<CousrsePagingDTO>> GetPagingCourses(int page, int pageSize, string sortBy)
        {
            var query = await JoinTableAndGetCourset();

            int totalRow = query.Count();
            /*if (totalRow < pageSize)
                return query;*/
            sortBy.ToUpper();
            if (sortBy == "VIEW")
                return query.OrderByDescending(x => x.View).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return query.OrderByDescending(x => x.CreateAt).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        //
        public async Task<List<CousrsePagingDTO>> GetByFilteringCousrse(string key)
        {
            var query =await JoinTableAndGetCourset();
            query.Where(m => m.Title.Contains(key) || m.FullName.Contains(key) || m.CategoryName.Contains(key)).ToList();
            return query;
        }
        // join table(course)->(user) && (course)->(category) && (enrollment)->(course) && (section)->(course) && (enrollment)->(courseReview) && (lecture)->(section)
        // and get CousrsePagingDTO
        private async Task<List<CousrsePagingDTO>> JoinTableAndGetCourset()
        {
            var query = await (from course in _context.Courses
                               join user in _context.Users on course.UserId equals user.Id
                               join category in _context.Categories on course.CategoryId equals category.Id
                               join enrollment in _context.Enrollment on course.Id equals enrollment.CourseId
                               join courseReview in _context.CourseReviews on enrollment.Id equals courseReview.EnrollmentId
                               join section in _context.Sections on course.Id equals section.CourseId
                               join lecture in _context.Lectures on section.Id equals lecture.SectionId
                               //where(course.Title.Contains(key))
                               //.GroupBy(p => p.course.Id)
                               //group course by course.Id into g
                               select new CousrsePagingDTO
                               {
                                   CousrseId = course.Id,
                                   FullName = user.Fullname,
                                   Title = course.Title,
                                   CategoryName = category.Name,
                                   ThumbnailUrl = course.ThumbnailUrl,
                                   Price = course.Price,
                                   Rating = courseReview.Rating,
                                   View = course.View,
                                   CreateAt = course.CreatedAt
                                   //TotalTime = lecture.TotalTime.
                               }).ToListAsync();

            //int totalRow = query.Count();
            return query;
        }
    }
}
