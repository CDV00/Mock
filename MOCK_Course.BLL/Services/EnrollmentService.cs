using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.DAL.Models;
using Course.BLL.DTO;
using Course.BLL.Responses;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;

namespace Course.BLL.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<EnrollmentDTO>> Add(Guid userId, Guid CourseId)
        {
            try
            {
                var enrollment = new Enrollment()
                {
                    UserId = userId,
                    CourseId = CourseId,
                    CreatedAt = DateTime.Now,
                };

                await _enrollmentRepository.CreateAsync(enrollment);
                await _unitOfWork.SaveChangesAsync();

                return new Response<EnrollmentDTO>(
                    true, _mapper.Map<EnrollmentDTO>(enrollment));
            }
            catch (Exception ex)
            {
                return new Response<EnrollmentDTO>(false, ex.Message, null);
            }
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
