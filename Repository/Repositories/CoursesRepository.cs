using AutoMapper;
using Course.BLL.Responses;
using Course.DAL.Data;
using Course.DAL.DTOs;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using SES.HomeServices.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CoursesRepository : Repository<Courses>, ICoursesRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CoursesRepository(AppDbContext context, IMapper mapper) : base(context)
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
