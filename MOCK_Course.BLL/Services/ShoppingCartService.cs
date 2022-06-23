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

        public async Task<ApiBaseResponse> Add(Guid userId, Guid courseId)
        {
            var course = await _cousesRepository.GetByIdAsync(courseId);

            if (course is null)
                return new CourseNotFoundResponse(courseId);

            if (course.IsFree.GetValueOrDefault())
                return new InvalidPriceCartResponse(courseId);

            if (await _shoppingCartRepository.BuildQuery()
                                             .FilterByCourseId(courseId)
                                             .FilterByUserId(userId)
                                             .AnyAsync())
                return new DuplicateCartResponse(courseId);

            var cart = new ShoppingCart()
            {
                UserId = userId,
                CourseId = courseId,
            };

            await _shoppingCartRepository.CreateAsync(cart);
            await _unitOfWork.SaveChangesAsync();

            var cartResponse = _mapper.Map<CartDTO>(cart);
            return new ApiOkResponse<CartDTO>(cartResponse);
        }

        public async Task<ApiBaseResponse> GetAllAsync(CartParameters parameters, Guid userId)
        {
            var carts = await _shoppingCartRepository.GetAllAsync(parameters, userId);

            return new ApiOkResponse<PagedList<CartDTO>>(carts);
        }
        public async Task<ApiBaseResponse> Update(Guid userId, CartUpdateRequest cartUpdate)
        {
            var courseId = cartUpdate.CourseId;
            var course = await _cousesRepository.GetByIdAsync(courseId);

            if (course is null)
                return new CourseNotFoundResponse(courseId);

            if (course.Price <= 0)
                return new InvalidPriceCartResponse(courseId);

            var cart = await _shoppingCartRepository.BuildQuery()
                                                    .FilterByUserId(userId)
                                                    .FilterByCourseId(cartUpdate.CourseId)
                                                    .AsSelectorAsync(c => c);

            _mapper.Map(cartUpdate, cart);
            await _unitOfWork.SaveChangesAsync();

            var cartResponse = _mapper.Map<CartDTO>(cart);

            return new ApiOkResponse<CartDTO>(cartResponse);
        }
        public async Task<ApiBaseResponse> Remove(Guid courseId, Guid userId)
        {
            var course = await _cousesRepository.GetByIdAsync(courseId);

            if (course is null)
                return new CourseNotFoundResponse(courseId);

            var cart = await _shoppingCartRepository.BuildQuery()
                                                    .FilterByUserId(userId)
                                                    .FilterByCourseId(courseId)
                                                    .AsSelectorAsync(c => c);

            _shoppingCartRepository.Remove(cart, permanent: true);
            await _unitOfWork.SaveChangesAsync();

            return new ApiBaseResponse(true);
        }
    }
}
