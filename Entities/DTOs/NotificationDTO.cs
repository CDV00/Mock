
using Course.BLL.DTO;
using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class NotificationDTO
    {
        public Guid UserId { get; set; }
        public string Messenge { get; set; }
        public UserDTO User { get; set; }
    }
    public class NotificationCountResult
    {
        public int Count { get; set; }
    }
}
