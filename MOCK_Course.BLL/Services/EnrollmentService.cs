using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.DAL.Models;
using Course.BLL.DTO;
using Course.BLL.Responses;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using Entities.Responses;

namespace Course.BLL.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICourseRepository _cousesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository,
                                 IMapper mapper,
                                 IUnitOfWork unitOfWork,
                                 ICourseRepository cousesRepository,
                                 IOrderRepository orderRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cousesRepository = cousesRepository;
            _orderRepository = orderRepository;
        }

        public async Task<ApiBaseResponse> Add(Guid userId, Guid CourseId)
        {
            var course = await _cousesRepository.GetByIdAsync(CourseId);

            var IsOrder = await _orderRepository.BuildQuery()
                                                .FilterByUserId(userId)
                                                .FilterByCourseId(CourseId)
                                                .AnyAsync();

            if (course is null)
                return new CourseNotFoundResponse(CourseId);

            if (!course.IsFree.HasValue && !IsOrder)
                return new InvalidEnrollCoursePrice(CourseId);

            //if ()
            //    return new NotOrderCourseResponse(CourseId);

            if (await _enrollmentRepository.BuildQuery()
                                           .FilterByCourseId(CourseId)
                                           .FilterByUserId(userId)
                                           .AnyAsync())
                return new DuplicateEnrollmentResponse(CourseId);

            var enrollment = new Enrollment()
            {
                UserId = userId,
                CourseId = CourseId
            };

            await _enrollmentRepository.CreateAsync(enrollment);

            course.TotalEnrolls++;

            await _unitOfWork.SaveChangesAsync();

            return new ApiOkResponse<EnrollmentDTO>(_mapper.Map<EnrollmentDTO>(enrollment));
        }

        private async Task<bool> CheckPriceGreaterZero(Guid courseId)
        {
            decimal price = await _cousesRepository.BuildQuery()
                                                   .FilterById(courseId)
                                                   .AsSelectorAsync(c => c.Price);
            if (price <= 0)
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
        public async Task<Responses<EnrollmentDTO>> GetAll(Guid? userId)
        {
            var enrollments = await _enrollmentRepository.BuildQuery()
                                                         .FilterByUserId(userId)
                                                         .IncludeUser()
                                                         .IncludeCourse()
                                                         .ToListAsync(x => _mapper.Map<EnrollmentDTO>(x));

            return new Responses<EnrollmentDTO>(true, enrollments);
        }
    }
}
