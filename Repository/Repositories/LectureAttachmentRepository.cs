using AutoMapper;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Repository.Repositories.Abstraction;

namespace Repository.Repositories
{
    public class LectureAttachmentRepository : Repository<LectureAttachment>, ILectureAttachmentRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public LectureAttachmentRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public ILectureAttachmentQuery BuildQuery()
        {
            return new LectureAttachmentQuery(_context.LectureAttachments.AsQueryable(), _context);
        }
    }
}
