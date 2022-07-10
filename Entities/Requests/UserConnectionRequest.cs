using System;

namespace Entities.Requests
{
    public class UserConnectionRequest
    {
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
    }



    public class JoinGroupRequest
    {
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
    }

}
