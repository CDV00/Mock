﻿using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;

namespace Course.DAL.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart, Guid>, IShoppingCartRepository
    {
        private AppDbContext _context;
        public ShoppingCartRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IShoppingCartQuery BuildQuery()
        {
            return new ShoppingCartQuery(_context.Carts.AsQueryable(), _context);
        }
    }
}
