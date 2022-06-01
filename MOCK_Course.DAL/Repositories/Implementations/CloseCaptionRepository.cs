﻿using Course.DAL.Data;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Course.DAL.Repositories.Implementations
{
    public class CloseCaptionRepository : Repository<CloseCaption, Guid>, ICloseCaptionRepository
    {
        private AppDbContext _context;
        public CloseCaptionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public override void Remove(CloseCaption _object)
        {
            if (_object == null)
                return;
            _context.CloseCaptions.Remove(_object);
        }

        public async Task<bool> RemoveAll(Guid courseId)
        {
            Entity().RemoveRange(GetAll().Where(a => a.CourseId == courseId));

            return true;
        }
    }
}
