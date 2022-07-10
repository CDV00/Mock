using Course.DAL.Models;
using System;

namespace Entities.Models
{
    public class MessageChat : BaseEntity<Guid>
    {
        public string Text { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }

        public Guid UserId { get; set; }
        public AppUser User { get; set; }
    }
}
