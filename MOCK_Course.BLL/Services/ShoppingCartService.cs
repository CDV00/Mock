using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.BLL.Services.Abstraction;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Course.BLL.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<CartDTO>> Add(Guid userId, Guid courseId)
        {
            try
            {
                var cart = new ShoppingCart()
                {
                    UserId = userId,
                    CourseId = courseId,
                };

                await _shoppingCartRepository.CreateAsync(cart);
                await _unitOfWork.SaveChangesAsync();

                var cartResponse = _mapper.Map<CartDTO>(cart);
                return new Response<CartDTO>(
                    true, cartResponse);
            }
            catch (Exception ex)
            {
                return new Response<CartDTO>(false, ex.Message, null);
            }
        }

        // todo:test
        public async Task<Responses<CartDTO>> GetAll(Guid userId)
        {
            try
            {
                var ShoppingCart = await _shoppingCartRepository.BuildQuery()
                                                                .FilterByUserId(userId)
                                                                .IncludeCourse()
                                                                .IncludeUser()
                                                                .IncludeCategory()
                                                                .IncludeDiscount()
                                                                .ToListAsync(c => _mapper.Map<CartDTO>(c));

                return new Responses<CartDTO>(true, ShoppingCart);
            }
            catch (Exception ex)
            {
                return new Responses<CartDTO>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> Remove(Guid Id)
        {
            try
            {
                var cart = await _shoppingCartRepository.GetByIdAsync(Id);
                if (cart is null)
                {
                    return new BaseResponse(false, null, "can't find cart");
                }

                _shoppingCartRepository.Remove(cart);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponse { IsSuccess = true };

            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }

    }
}
