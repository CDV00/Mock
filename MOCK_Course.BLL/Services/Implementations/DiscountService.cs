using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.BLL.Responses;
using Course.BLL.Services.Interface;
using Coursess.DAL.Repositories.Abstraction;
using Course.DAL.Repositories;

namespace Course.BLL.Services.Implementations
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<DiscountDTO>> Add(DiscountForCreateRequest discountForCreateRequest)
        {
            try
            {
                var discount = _mapper.Map<Discount>(discountForCreateRequest);


                await _discountRepository.CreateAsync(discount);

                var result = await _unitOfWork.SaveChangesAsync();

                var DiscountDTO = _mapper.Map<DiscountDTO>(discount);
                return new Response<DiscountDTO>(
                    true,
                    DiscountDTO
                );
            }
            catch (Exception ex)
            {
                return new Response<DiscountDTO>(false, ex.Message, null);
            }
        }


        // Get All theo UserId !!!
        public async Task<Responses<DiscountDTO>> GetAllDiscount(Guid userId)
        {
            try
            {
                var discounts = await _discountRepository.BuildQuery()
                                                         .IncludeCourses()
                                                         .FilterByUserId(userId)
                                                         .ToListAsync(d => _mapper.Map<DiscountDTO>(d));

                return new Responses<DiscountDTO>(true, discounts);
            }
            catch (Exception ex)
            {
                return new Responses<DiscountDTO>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> Remove(Guid id)
        {
            try
            {
                var discount = await _discountRepository.GetByIdAsync(id);
                if (discount == null)
                {
                    new Responses<BaseResponse>(false, "can't find discount", null);
                }

                _discountRepository.Remove(discount);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponse(true, "Delete success", null);

            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<DiscountDTO>> Update(Guid id, DiscountForUpdateRequest discountForUpdateRequest)
        {
            try
            {
                var discount = await _discountRepository.GetByIdAsync(id);
                if (discount == null)
                {
                    new Responses<DiscountDTO>(false, "can't find discount", null);
                }


                _mapper.Map(discountForUpdateRequest, discount);

                await _unitOfWork.SaveChangesAsync();

                var DiscountResponse = _mapper.Map<DiscountDTO>(discount);

                return new Response<DiscountDTO>(
                    true,
                    DiscountResponse
                );
            }
            catch (Exception ex)
            {
                return new Response<DiscountDTO>(false, ex.Message, null);
            }
        }
    }
}
