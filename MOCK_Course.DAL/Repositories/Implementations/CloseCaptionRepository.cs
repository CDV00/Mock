﻿using Course.DAL.Data;
using Course.DAL.Models;
using System;

namespace Course.DAL.Repositories.Implementations
{
    public class CloseCaptionRepository : Repository<CloseCaption, Guid>, ICloseCaptionRepository
    {
        private AppDbContext _context;
        public CloseCaptionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
