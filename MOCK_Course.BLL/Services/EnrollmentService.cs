using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.DAL.Models;
using Course.BLL.DTO;
using Course.BLL.Responses;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using Repository.Repositories;

namespace Course.BLL.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICousesRepository _cousesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICousesRepository cousesRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cousesRepository = cousesRepository;
        }

        public async Task<Response<EnrollmentDTO>> Add(Guid userId, Guid CourseId)
        {
            try
            {
                if (checkPrice(CourseId).Result)
                {
                    return new Response<EnrollmentDTO>(false,"", null);
                }
                var enrollment = new Enrollment()
                {
                    UserId = userId,
                    CourseId = CourseId,
                    CreatedAt = DateTime.Now,
                };

                await _enrollmentRepository.CreateAsync(enrollment);

                var course = await _cousesRepository.GetByIdAsync(CourseId);
                course.TotalEnrolls++;

                await _unitOfWork.SaveChangesAsync();

                return new Response<EnrollmentDTO>(
                    true, _mapper.Map<EnrollmentDTO>(enrollment));
            }
            catch (Exception ex)
            {
                return new Response<EnrollmentDTO>(false, ex.Message, null);
            }
        }
        //
        private async Task<bool> checkPrice(Guid courseId)
        {
            decimal price = await _cousesRepository.BuildQuery()
                                                   .FilterById(courseId)
                                                   .AsSelectorAsync(c => c.Price);
            if (price == 0)
                return true; 
            return false;
        } 
        public async Task<Response<int>> GetTotalEnrollOfUser(Guid userId)
        {
            try
            {
                var count = await _enrollmentRepository.BuildQuery()
                                                       .FilterByUserId(userId)
                                                       .CountAsync();
                return new Response<int>(true, count);
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }

        public async Task<Response<int>> GetTotalEnrollOfInstructor(Guid userId)
        {
            try
            {
                var count = await _enrollmentRepository.BuildQuery()
                                                       .FilterByCourseUserId(userId)
                                                       .CountAsync();
                return new Response<int>(true, count);
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }

        public async Task<Response<int>> GetTotalEnrollOfCourse(Guid courseId)
        {
            try
            {
                var count = await _enrollmentRepository.BuildQuery()
                                                       .FilterByCourseId(courseId)
                                                       .CountAsync();
                return new Response<int>(true, count);
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }

        public async Task<Response<EnrollmentDTO>> IsEnrollment(Guid userId, Guid courseId)
        {
            try
            {
                var enrollment = await _enrollmentRepository.BuildQuery()
                                                            .FilterByUserId(userId)
                                                            .FilterByCourseId(courseId)
                                                            .AsSelectorAsync(e => _mapper.Map<EnrollmentDTO>(e));

                if (enrollment == null)
                {
                    return new Response<EnrollmentDTO>(false, enrollment);
                }

                return new Response<EnrollmentDTO>(true, enrollment);
            }
            catch (Exception ex)
            {
                return new Response<EnrollmentDTO>(false, ex.Message, null);
            }
        }
    }
}
