using Course.BLL.DTO;
using Entities.DTOs;
using Entities.Requests;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public interface IRoomService
    {
        Task<Response<RoomDTO>> Add(RoomRequest roomRequest, Guid userId);
        Task<Responses<RoomDTO>> GetByRoomId(Guid id);
        Task<Responses<RoomMessageDTO>> GetByUserId(Guid userId);
    }
}