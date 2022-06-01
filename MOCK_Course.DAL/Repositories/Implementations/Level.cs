﻿using Course.DAL.Data;
using Course.DAL.DTOs;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Course.DAL.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.Implementations
{
    public class LevelRepository : Repository<Level, Guid>, ILevelRepository
    {
        private AppDbContext _context;
        public LevelRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        async Task<IList<LevelDTO>> ILevelRepository.GetAll()
        {
            return await GetAll().Select(l => new LevelDTO() { Id = l.Id, Name = l.Name }).ToListAsync();
        }
    }
}
