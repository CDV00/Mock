using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.BLL.Services.Abstraction;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Entities.ParameterRequest;
using Entities.Responses;
using Repository.Repositories;

namespace Course.BLL.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICourseRepository _cousesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICourseRepository cousesRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cousesRepository = cousesRepository;
        }

        public async Task<Response<CartDTO>> Add(Guid userId, Guid courseId)
        {
            try
            {
                if (await _cousesRepository.BuildQuery()
                                           .FilterById(courseId)
                                           .AsSelectorAsync(c => c.Price) <= 0)
                    return new Response<CartDTO>(false, "Can't add course with price == 0", null);

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

        public async Task<ApiBaseResponse> GetAllAsync(CartParameters parameters, Guid userId)
        {
            var carts = await _shoppingCartRepository.GetAllAsync(parameters, userId);

            return new ApiOkResponse<PagedList<CartDTO>>(carts);
        }
        public async Task<Response<CartDTO>> Update(Guid userId, CartUpdateRequest cartUpdateRequest)
        {
            try
            {
                var cart = await _shoppingCartRepository.BuildQuery()
                                                        .FilterByUserId(userId)
                                                        .FilterByCourseId(cartUpdateRequest.CourseId)
                                                        .AsSelectorAsync(c => c);

                if (cart == null)
                {
                    new Responses<CartDTO>(false, "Can't find cart", null);
                }


                _mapper.Map(cartUpdateRequest, cart);
                await _unitOfWork.SaveChangesAsync();

                var cartResponse = _mapper.Map<CartDTO>(cart);

                return new Response<CartDTO>(
                    true,
                    cartResponse
                );
            }
            catch (Exception ex)
            {
                return new Response<CartDTO>(false, ex.Message, null);
            }
        }
        public async Task<BaseResponse> Remove(Guid courseId, Guid userId)
        {
            try
            {
                var cart = await _shoppingCartRepository.BuildQuery()
                                                        .FilterByUserId(userId)
                                                        .FilterByCourseId(courseId)
                                                        .AsSelectorAsync(c => c);
                if (cart is null)
                {
                    return new BaseResponse(false, null, "Can't find cart");
                }

                if (cart.UserId != userId)
                    return new Response<BaseResponse>(false, "You aren't the owner of the shopping cart", null);

                _shoppingCartRepository.Remove(cart, true);
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
