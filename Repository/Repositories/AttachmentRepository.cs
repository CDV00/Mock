using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using System;

namespace Repository.Repositories
{
    public class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
    {
        private AppDbContext _context;
        public AttachmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IAttachmentQuery BuildQuery()
        {
            return new AttachmentQuery(_context.Attachments.AsQueryable(), _context);
        }

    }
}
