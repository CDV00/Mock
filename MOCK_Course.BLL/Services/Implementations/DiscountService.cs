using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Course.BLL.Responses;
using Course.DAL.DTOs;
using Microsoft.AspNetCore.Identity;

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

        public async Task<Response<DiscountForCreateDTO>> Add(DiscountForCreateRequest discountForCreateRequest)
        {
            try
            {
                var discount = _mapper.Map<Discount>(discountForCreateRequest);
                

                await _discountRepository.CreateAsync(discount);

                var result = await _unitOfWork.SaveChangesAsync();

                var DiscountDTO = _mapper.Map<DiscountForCreateDTO>(discount);
                return new Response<DiscountForCreateDTO>(
                    true,
                    DiscountDTO
                );
            }
            catch (Exception ex)
            {
                return new Response<DiscountForCreateDTO>(false, ex.Message, null);
            }
        }


        public async Task<Responses<DiscountDTO>> GetAllDiscount()
        {
            try
            {
                var discount = await _discountRepository.GetAllDiscount();

                var discountResponse = _mapper.Map<List<DiscountDTO>>(discount);
                return new Responses<DiscountDTO>(true, discountResponse);
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

        public async Task<Response<DiscountForUpdateDTO>> Update(Guid discountId, DiscountForUpdateRequest discountForUpdateRequest)
        {
            try
            {
                var discount = await _discountRepository.GetByIdAsync(discountId);
                if (discount == null)
                {
                    new Responses<BaseResponse>(false, "can't find discount", null);
                }


                _mapper.Map(discountForUpdateRequest, discount);

                await _unitOfWork.SaveChangesAsync();

                var DiscountResponse = _mapper.Map<DiscountForUpdateDTO>(discount);

                return new Response<DiscountForUpdateDTO>(
                    true,
                    DiscountResponse
                );
            }
            catch (Exception ex)
            {
                return new Response<DiscountForUpdateDTO>(false, ex.Message, null);
            }
        }

    }
}
