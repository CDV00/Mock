using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Course.BLL.Services.Implementations
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

        public async Task<Response<CartResponse>> Add(CartRequest cartRequest)
        {
            try
            {
                var cart = _mapper.Map<ShoppingCart>(cartRequest);
                await _shoppingCartRepository.CreateAsync(cart);
                await _unitOfWork.SaveChangesAsync();

                var cartResponse = _mapper.Map<CartResponse>(cart);
                return new Response<CartResponse>(
                    true, cartResponse);
            }
            catch (Exception ex)
            {
                return new Response<CartResponse>(false, ex.Message, null);
            }
        }

        public async Task<Responses<CartResponse>> GetAll(Guid userId)
        {
            try
            {
                var result = await _shoppingCartRepository.GetAll().Where(s => s.UserId == userId).Include(s => s.User).Include(s => s.Course).Include(s => s.User).Include(s => s.Course.Category).ToListAsync();

                return new Responses<CartResponse>(true, _mapper.Map<IEnumerable<CartResponse>>(result));
            }
            catch (Exception ex)
            {
                return new Responses<CartResponse>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> Remove(Guid IdShoppingCart)
        {
            try
            {
                var cart = await _shoppingCartRepository.GetByIdAsync(IdShoppingCart);
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
