using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.BLL.Responses;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using Entities.Responses;
using System.Linq;
using Course.BLL.Share.RequestFeatures;
using Entities.ParameterRequest;

namespace Course.BLL.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _coursesRepository;
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public DiscountService(ICourseRepository coursesRepository, IDiscountRepository discountRepository, IMapper mapper, IUnitOfWork unitOfWork, ICourseService courseService)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _coursesRepository = coursesRepository;
            _courseService = courseService;
        }

        public async Task<ApiBaseResponse> Add(DiscountForCreateRequest discount)
        {
            if (!await _coursesRepository.IsExist(discount.CourseId))
                return new CourseNotFoundResponse(discount.CourseId);

            if (await _discountRepository.CheckDiscountTimeExisting(discount.CourseId, discount.StartDate, discount.EndDate, null))
                return new ExistDiscountTimeResponse(discount.StartDate, discount.EndDate, discount.CourseId);

            var discountEntity = _mapper.Map<Discount>(discount);
            discountEntity.CreatedAt = DateTime.Now;

            await _discountRepository.CreateAsync(discountEntity);
            await _unitOfWork.SaveChangesAsync();

            var discountDTO_ = _mapper.Map<DiscountDTO_>(discountEntity);
            return new ApiOkResponse<DiscountDTO_>(discountDTO_);
        }


        public async Task<ApiBaseResponse> GetAllDiscount(Guid UserId, DiscountParameters parameter)
        {
            var discounts = await _discountRepository.GetAllDiscount(UserId, parameter);

            return new ApiOkResponse<PagedList<DiscountDTO_>>(discounts);
        }

        public async Task<ApiBaseResponse> Remove(Guid id)
        {
            var discountEntity = await _discountRepository.GetByIdAsync(id);
            if (discountEntity is null)
                return new DiscountNotFound(id);

            if (!await _coursesRepository.IsExist(discountEntity.CourseId))
                return new CourseNotFoundResponse(discountEntity.CourseId);

            _discountRepository.Remove(discountEntity, false);
            await _unitOfWork.SaveChangesAsync();

            return new ApiBaseResponse(true);
        }

        public async Task<ApiBaseResponse> Update(Guid id, DiscountForUpdateRequest discount)
        {
            var discountEntity = await _discountRepository.GetByIdAsync(id);

            if (discountEntity is null)
                return new DiscountNotFound(id);

            if (!await _coursesRepository.IsExist(discountEntity.CourseId))
                return new CourseNotFoundResponse(discountEntity.CourseId);

            if (await _discountRepository.CheckDiscountTimeExisting(discountEntity.CourseId, discount.StartDate, discount.EndDate, id))
                return new ExistDiscountTimeResponse(discount.StartDate, discount.EndDate, discountEntity.CourseId);

            _mapper.Map(discount, discountEntity);
            discountEntity.UpdatedAt = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();

            var DiscountDTO = _mapper.Map<DiscountDTO_>(discountEntity);

            return new ApiOkResponse<DiscountDTO_>(DiscountDTO);
        }
    }
}
