﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class Repository<T, K> : IRepository<T, K> where T : BaseEntity<K>
    {
        protected DbSet<T> DbSet;
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            DbSet = context.Set<T>();
            _context = context;
        }

        public DbSet<T> Entity()
        {
            return DbSet;
        }

        public virtual async Task CreateAsync(T _object) => await DbSet.AddAsync(_object);

        public virtual async Task CreateRangeAsync(List<T> _object) => await DbSet.AddRangeAsync(_object);

        public virtual void Remove(T _object)
        {
            _context.Entry(_object).State = EntityState.Modified;
            _object.IsDeleted = true;
        }

        public virtual void RemoveRange(List<T> _object)
        {
            _context.Entry(_object).State = EntityState.Modified;
            for (var i = 0; i < _object.Count; i++)
            {
                _object[0].IsDeleted = true;
            }
        }


        public virtual IQueryable<T> GetAll() => DbSet.AsNoTracking();

        public virtual async Task<T> GetByIdAsync(K Id)
        {
            var data = await DbSet.FindAsync(Id);
            if (data == null) return null;
            _context.Entry(data).State = EntityState.Modified;
            return data;
        }

        public virtual bool Update(T _object)
        {
            DbSet.Attach(_object);
            _context.Entry(_object).State = EntityState.Modified;
            return true;
        }

        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => DbSet.Where(expression).AsNoTracking();

        public virtual async Task<int> CountAsync() => await DbSet.CountAsync().ConfigureAwait(false);

        public virtual async Task<bool> IsExists() => await DbSet.AnyAsync();
    }
}