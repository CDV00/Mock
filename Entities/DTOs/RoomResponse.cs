using Course.DAL.Models;
using System;
using System.Collections.Generic;

namespace Entities.DTOs
{
    public class MessageChatDTO
    {
        public string Text { get; set; }
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
    }
    public class ParticipantDTO
    {
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
    }

    public class RoomDTO
    {
        public string Name { get; set; }
        public ICollection<MessageChatDTO> MessageChats { get; set; }
        public ICollection<ParticipantDTO> Participants { get; set; }
    }
    public class RoomResponse
    {
        public RoomDTO Data { get; set; }
    }
    public class ListRoomResponse
    {
        public IList<RoomDTO> Data { get; set; }
    }
    public class ListParticipantRoomResponse
    {
        public IList<RoomMessageDTO> Data { get; set; }
    }
    public class RoomMessageDTO
    {
        public string Name { get; set; }
        public ICollection<MessageChatDTO> MessageChats { get; set; }
    }
}
