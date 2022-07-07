using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.DAL.Models;
using Course.BLL.DTO;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using Course.BLL.Responses;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Course.BLL.Share.RequestFeatures;
using Entities.ParameterRequest;
using Microsoft.EntityFrameworkCore;
using Entities.DTOs;
using Entities.Responses;

namespace Course.BLL.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICourseService _courseService;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork, ICourseService courseService, UserManager<AppUser> userManager)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _courseService = courseService;
        }

        public async Task<Response<SubscriptionDTO>> Add(Guid userId, Guid instructorId)
        {
            try
            {
                var IsExits = await _subscriptionRepository.BuildQuery()
                                                           .FilterByUserId(instructorId)
                                                           .FilterBySubscriberId(userId)
                                                           .AnyAsync();

                if (IsExits)
                    return new Response<SubscriptionDTO>(false, "You are already subscription", null);

                var subscription = new Subscription()
                {
                    UserId = instructorId,
                    SubscriberId = userId,
                    CreatedAt = DateTime.UtcNow,
                };

                await _subscriptionRepository.CreateAsync(subscription);
                await _unitOfWork.SaveChangesAsync();
                return new Response<SubscriptionDTO>(
                    true, _mapper.Map<SubscriptionDTO>(subscription));
            }
            catch (Exception ex)
            {
                return new Response<SubscriptionDTO>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> Delete(Guid userId, Guid instructorId)
        {
            try
            {
                var subscription = await _subscriptionRepository.BuildQuery()
                                                                .FilterByUserId(instructorId)
                                                                .FilterBySubscriberId(userId)
                                                                .AsSelectorAsync(s => s);

                if (subscription is null)
                {
                    return new BaseResponse(false, null, "can't find subscription");
                }
                _subscriptionRepository.Remove(subscription, true);

                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
        }

        public async Task<Response<int>> GetTotalSubscriber(Guid userId)
        {
            try
            {
                var total = await _subscriptionRepository.BuildQuery()
                                                         .FilterByUserId(userId)
                                                         .CountAsync();

                return new Response<int>(true, total);
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }

        public async Task<Response<int>> GetTotalInstructor(Guid subscriberId)
        {
            var total = await _subscriptionRepository.BuildQuery()
                                                     .FilterBySubscriberId(subscriberId)
                                                     .CountAsync();

            return new Response<int>(true, total);
        }

        public async Task<Response<SubscriptionDTO>> IsSubscription(Guid userId, Guid instructorId)
        {
            try
            {
                var subscription = await _subscriptionRepository.BuildQuery()
                                                                .FilterByUserId(instructorId)
                                                                .FilterBySubscriberId(userId)
                                                                .AsSelectorAsync(s => _mapper.Map<SubscriptionDTO>(s));

                return new Response<SubscriptionDTO>(true, subscription);
            }
            catch (Exception ex)
            {
                return new Response<SubscriptionDTO>(false, ex.Message, null);
            }
        }



        public async Task<PagedList<UserDTO>> GetAllSubscriber(Guid userId, SubscriptionParameters subscriptionParameters)
        {
            var user = await _subscriptionRepository.BuildQuery()
                                                    .FilterByUserId(userId)
                                                    .FilterByUserByKeyword(subscriptionParameters.Keyword)
                                                    .IncludeSubcriber()
                                                    .ToListAsync(u => _mapper.Map<UserDTO>(u.Subscriber));

            var count = await _subscriptionRepository.BuildQuery()
                                                     .FilterByUserId(userId)
                                                     .FilterByUserByKeyword(subscriptionParameters.Keyword)
                                                     .CountAsync();

            for (var i = 0; i < user.Count; i++)
            {
                user[i].TotalSubcripbers = (await GetTotalSubscriber(user[i].Id)).data;
                user[i].TotalCourses = (await _courseService.GetTotalCourseOfUser(user[i].Id)).data;
            }

            return new PagedList<UserDTO>(user, count, subscriptionParameters.PageNumber, subscriptionParameters.PageSize);
        }

        public async Task<PagedList<UserDTO>> GetAllInstructor(SubscriptionParameters subscriptionParameters, Guid userId)
        {
            var user = await _subscriptionRepository.BuildQuery()
                                                    .FilterBySubscriberId(userId)
                                                    .FilterBySubscriber(subscriptionParameters.Keyword)
                                                    .IncludeInstructor()
                                                    .ToListAsync(u => _mapper.Map<UserDTO>(u.User));


            var count = await _subscriptionRepository.BuildQuery()
                                                     .FilterBySubscriberId(userId)
                                                     .FilterBySubscriber(subscriptionParameters.Keyword)
                                                     .CountAsync();
            for (var i = 0; i < user.Count; i++)
            {
                user[i].TotalSubcripbers = (await GetTotalSubscriber(user[i].Id)).data;
                user[i].TotalCourses = (await _courseService.GetTotalCourseOfUser(user[i].Id)).data;
            }


            return new PagedList<UserDTO>(user, count, subscriptionParameters.PageNumber, subscriptionParameters.PageSize);
        }
        public async Task<Response<SubscriptionDTO>> IsSubscribed(Guid userId, Guid instructorId)
        {
            try
            {
                var subscription = await _subscriptionRepository.BuildQuery()
                                                                .FilterByUserId(instructorId)
                                                                .FilterBySubscriberId(userId)
                                                                .AsSelectorAsync(s => _mapper.Map<SubscriptionDTO>(s));

                if (subscription == null)
                {
                    return new Response<SubscriptionDTO>(false, subscription);
                }

                return new Response<SubscriptionDTO>(true, subscription);
            }
            catch (Exception ex)
            {
                return new Response<SubscriptionDTO>(false, ex.Message, null);
            }
        }

        public async Task<ApiOkResponse<ListAnalysisSubscriberResponse>> CountSubcriberByMonth(Guid subscriberId)
        {
            var filterdata = await _subscriptionRepository.FindByCondition(j => j.SubscriberId == subscriberId).ToListAsync();

            var yearAndMonthRange = filterdata.Select(m => m.CreatedAt.Year).Distinct().OrderBy(t => t).Select(m => Enumerable.Range(1, 12).Select(t => new { year = m, month = t })).ToList();

            var result = yearAndMonthRange.Select(m => m.GroupJoin(filterdata, key => key, f => new { year = f.CreatedAt.Year, month = f.CreatedAt.Month }, (key, f) => new AnalysisSubscriberDTO { Year = key.year, Month = key.month, Amount = f.Count() }).OrderBy(d => d.Year).ThenBy(d => d.Month).ToList()).ToList();

            var totalCount = _subscriptionRepository.FindByCondition(j => j.SubscriberId == subscriberId).Count();

            return new ApiOkResponse<ListAnalysisSubscriberResponse>(new ListAnalysisSubscriberResponse()
            {
                Data = result,
                Total = totalCount
            });
        }
    }
}
