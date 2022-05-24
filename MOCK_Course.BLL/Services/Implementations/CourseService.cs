using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Responsesnamespace;
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
        private readonly ISectionService _sectionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseService(ICousesRepository cousesRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork, ISectionService sectionService)
        {
            _cousesRepository = cousesRepository;
            _sectionService = sectionService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Responses<CoursesResponse>> GetAll()
        {
            try
            {
                var categories = await _cousesRepository.GetAll().Include(c => c.User).Include(c => c.Category).ToListAsync();

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

                if (courseRequest.SectionRequests.Count > 0)
                {
                    foreach (var section in courseRequest.SectionRequests)
                    {
                        await _sectionService.Add(section);
                    }
                }

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

        public async Task<Responsesnamespace.BaseResponse> Remove(Guid idCourse)
        {
            try
            {
                var result = await _cousesRepository.GetByIdAsync(idCourse);

                _cousesRepository.Remove(result);
                _unitOfWork.SaveChanges();

                return new Responsesnamespace.BaseResponse { IsSuccess = true };

            }
            catch (Exception ex)
            {
                return new Responses<Responsesnamespace.BaseResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<SectionResponse>> Update(CourseUpdateRequest courseRequest)
        {
            try
            {
                var course = _mapper.Map<Courses>(courseRequest);

                _cousesRepository.Update(course);

                if (courseRequest.SectionRequests.Count > 0)
                {
                    foreach (var section in courseRequest.SectionRequests)
                    {
                        await _sectionService.Update(section);
                    }
                }
                await _unitOfWork.SaveChangesAsync();
                return new Response<SectionResponse>(
                    true,
                    _mapper.Map<SectionResponse>(course)
                );
            }
            catch (Exception ex)
            {
                return new Response<SectionResponse>(false, ex.Message, null);
            }
        }
    }
}
