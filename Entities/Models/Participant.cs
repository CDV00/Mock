using Course.DAL.Models;
using System;

namespace Entities.Models
{
    public class Participant
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid RoomId { get; set; }
        public Room Room { get; set; }
    }
}
