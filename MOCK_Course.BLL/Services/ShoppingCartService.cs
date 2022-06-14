using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.BLL.Services.Abstraction;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Repository.Repositories;

namespace Course.BLL.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICousesRepository _cousesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICousesRepository cousesRepository)
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

                //TODO: CHECK EXIST CART

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

        // TODO: Paging and filter by name course
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
        public async Task<Response<CartDTO>> Update(Guid id, CartUpdateRequest cartUpdateRequest)
        {
            try
            {
                var cart = await _shoppingCartRepository.GetByIdAsync(id);

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
        public async Task<BaseResponse> Remove(Guid Id, Guid userId)
        {
            try
            {
                var cart = await _shoppingCartRepository.GetByIdAsync(Id);
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
