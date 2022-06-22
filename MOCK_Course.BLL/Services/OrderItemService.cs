using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Course.DAL.Models;
using System;
using System.Threading.Tasks;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using System.Linq;
using System.Collections.Generic;
using Repository.Repositories;
using MOCK_Course.BLL.Services.Implementations;
using Microsoft.AspNetCore.Identity;

namespace Course.BLL.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public OrderItemService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IOrderItemRepository orderItemRepository,
            UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

            public async Task<Response<OrderItemDTO>> IsPurchased(Guid userId, Guid courseId)
            {
                try
                {
                    var purchase = await _orderItemRepository.BuildQuery()
                                                           .FilterByUserId(userId)
                                                           .FilterByCourseId(courseId)
                                                           .AsSelectorAsync(e => _mapper.Map<OrderItemDTO>(e));

                if (purchase == null)
                {
                    return new Response<OrderItemDTO>(true, null);
                }

                return new Response<OrderItemDTO>(true, purchase);
            }
                catch (Exception ex)
                {
                    return new Response<OrderItemDTO>(false, ex.Message, null);
                }
            }
        }
}
