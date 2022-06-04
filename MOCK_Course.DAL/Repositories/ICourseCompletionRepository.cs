﻿using Course.DAL.Models;
using Course.DAL.Queries;
using System;
using System.Threading.Tasks;

namespace Course.DAL.Repositories
{
    public interface ICourseCompletionRepository : IRepository<CourseCompletion, Guid>
    {
    }
}
