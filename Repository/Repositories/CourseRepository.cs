using AutoMapper;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Repositories.Abstraction;

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
    }
}
