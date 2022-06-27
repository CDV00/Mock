using AutoMapper;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Data;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using Entities.DTOs;
using Entities.ParameterRequest;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public NotificationRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }


        public INotificationQuery BuildQuery()
        {
            return new NotificationQuery(_context.Notifications.AsQueryable(), _context);
        }

        /*public async Task<PagedList<NotificationDTO>> GetAllNotification(NotificationParameters parameters)
        {
            var notifications = await BuildQuery().FilterByKeyword(parameters.Keyword)
                                            .IncludeToUser()
                                            .IncludeFromUser()
                                            .ApplySort(parameters.Orderby)
                                            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                            .Take(parameters.PageSize)
                                            .ToListAsync(c => _mapper.Map<NotificationDTO>(c));

            var count = await BuildQuery().FilterByKeyword(parameters.Keyword)
                                          .CountAsync();

            return new PagedList<NotificationDTO>(notifications, count, parameters.PageNumber, parameters.PageSize);
        }*/

    }
}
