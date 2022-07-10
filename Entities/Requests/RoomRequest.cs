using System;

namespace Entities.Requests
{
    public class RoomRequest
    {
        //public Guid Id { get; set; } == null;
        //public string Name { get; set; }
        public ParticipantRequest Participant { get; set; }
    }

    public class ParticipantRequest
    {
        public Guid UserId { get; set; }
        //public Guid RoomId { get; set; }
    }
}
