//using Course.DAL.Data;
//using Course.DAL.Models;
//using CourseAPI.Extensions.ControllerBase;
//using Entities.DTOs;
//using Entities.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CourseAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class NotificationsController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public NotificationsController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Notifications/notificationcount
//        [Route("notificationcount")]
//        [HttpGet]
//        public async Task<ActionResult<NotificationCountResult>> GetNotificationCount()
//        {
//            var userId = User.GetUserId();
//            var count = (from not in _context.Notifications
//                         where not.UserId == userId
//                         select not).CountAsync();
//            NotificationCountResult result = new NotificationCountResult
//            {
//                Count = await count
//            };
//            return result;
//        }

//        // GET: api/Notifications/notificationresult
//        [Route("notificationresult")]
//        [HttpGet]
//        public async Task<ActionResult<List<NotificationDTO>>> GetNotificationMessage()
//        {
//            var userId = User.GetUserId();
//            var results = from message in _context.Notifications
//                          where (message.UserId == userId)
//                          orderby message.Id descending
//                          select new NotificationDTO
//                          {
//                              UserId = message.UserId,
//                              Messenge = message.Messenge
//                          };
//            return await results.ToListAsync();
//        }

//        // DELETE: api/Notifications/deletenotifications
//        [HttpDelete]
//        [Route("deletenotifications")]
//        public async Task<IActionResult> DeleteNotifications()
//        {
//            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Notifications");
//            await _context.SaveChangesAsync();
//            await _hubContext.Clients.All.BroadcastMessage();

//            return NoContent();
//        }
//    }
//}
