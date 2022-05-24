using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Responses;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Course.BLL.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICousesRepository _cousesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseService(ICousesRepository cousesRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _cousesRepository = cousesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Responses<CoursesResponse>> GetAll()
        {
            try
            {
                var categories = await _cousesRepository.GetAll().Include(c=>c.User).Include(c=>c.Category).ToListAsync();

                var courseResponse = _mapper.Map<List<CoursesResponse>>(categories);
                return new Responses<CoursesResponse>(true, courseResponse);
            }
            catch (Exception ex)
            {
                return new Responses<CoursesResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<CoursesResponse>> Add(CourseRequest courseRequest)
        {
            try
            {
                var course = _mapper.Map<Courses>(courseRequest);

                await _cousesRepository.CreateAsync(course);
                await _unitOfWork.SaveChangesAsync();
                return new Response<CoursesResponse>(
                    true,
                    _mapper.Map<CoursesResponse>(course)
                );
            }
            catch (Exception ex)
            {
                return new Response<CoursesResponse>(false, ex.Message, null);
            }
        }
    }
}
