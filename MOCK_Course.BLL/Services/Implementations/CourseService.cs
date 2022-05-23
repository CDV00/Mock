using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Responses;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

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
                var result = await _cousesRepository.GetAll().ToListAsync();
                return new Responses<CoursesResponse>(true, _mapper.Map<IEnumerable<CoursesResponse>>(result));
            }
            catch (Exception ex)
            {
                return new Responses<CoursesResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<CoursesResponse>> Add(CoursesRequest coursesRequest)
        {
            try
            {
                var course = _mapper.Map<Courses>(coursesRequest);

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
