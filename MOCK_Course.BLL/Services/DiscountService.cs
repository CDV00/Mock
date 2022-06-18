using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.BLL.Responses;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;

namespace Course.BLL.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _coursesRepository;
        private readonly IMapper _mapper;

        public DiscountService(ICourseRepository coursesRepository, IDiscountRepository discountRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _coursesRepository = coursesRepository;
        }

        public async Task<Response<DiscountDTO_>> Add(DiscountForCreateRequest discountForCreateRequest, Courses course)
        {
            var discount = _mapper.Map<Discount>(discountForCreateRequest);
            discount.CourseId = course.Id;
            discount.CreatedAt = DateTime.Now;

            await _discountRepository.CreateAsync(discount);

            await _unitOfWork.SaveChangesAsync();

            var DiscountDTO_ = _mapper.Map<DiscountDTO_>(discount);
            return new Response<DiscountDTO_>(
                true,
                DiscountDTO_
                );
        }


        public async Task<Responses<DiscountDTO_>> GetAllDiscount(Guid UserId)
        {
            var discounts = await _discountRepository.BuildQuery()
                                                     .IncludeCourses()
                                                     .FilterByUserId(UserId)
                                                     .ToListAsync(d => _mapper.Map<DiscountDTO_>(d));

            return new Responses<DiscountDTO_>(true, discounts);
        }

        public async Task<BaseResponse> Remove(Discount discount)
        {
            _discountRepository.Remove(discount, false);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse(true, "Delete success", null);
        }

        public async Task<Response<DiscountDTO_>> Update(Discount discount, DiscountForUpdateRequest discountForUpdateRequest)
        {
            _mapper.Map(discountForUpdateRequest, discount);
            discount.UpdatedAt = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();

            var DiscountResponse = _mapper.Map<DiscountDTO_>(discount);

            return new Response<DiscountDTO_>(
                true,
                DiscountResponse);
        }
    }
}
