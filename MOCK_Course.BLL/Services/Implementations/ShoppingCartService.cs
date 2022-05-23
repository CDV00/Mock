using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Responses;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Course.DAL.Data;

namespace Course.BLL.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork, AppDbContext context)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<Response<CartResponse>> Add(CartRequest cartRequest)
        {
            try
            {

                var cart = _mapper.Map<ShoppingCart>(cartRequest);
                await _shoppingCartRepository.CreateAsync(cart);
                await _unitOfWork.SaveChangesAsync();
                return new Response<CartResponse>(
                    true,
                    _mapper.Map<CartResponse>(cart)
                );            
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
                // GEt Shopping cart theo Us
                var result = await _shoppingCartRepository.GetAll().Where(s => s.UserId == userId).Include(s=>s.User).Include(s=>s.Course).ToListAsync();

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
                var result = await _shoppingCartRepository.GetByIdAsync(IdShoppingCart);
                if(result != null)
                {
                    result.IsDeleted = true;
                }
                return new BaseResponse { IsSuccess = true };
                
            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }

    }
}
