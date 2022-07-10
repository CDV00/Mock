using AutoMapper;
using Course.BLL.DTO;
using Course.DAL.Repositories.Abstraction;
using Entities.DTOs;
using Entities.Models;
using Entities.Requests;
using Microsoft.EntityFrameworkCore;
using MOCK_Course.DAL.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMessageChatRepository _messageChatRepository;

        public RoomService(IRoomRepository roomRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IParticipantRepository participantRepository,
            IMessageChatRepository messageChatRepository)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _participantRepository = participantRepository;
            _messageChatRepository = messageChatRepository;
        }

        public async Task<Responses<RoomMessageDTO>> GetByUserId(Guid userId)
        {
            var rooms = await _participantRepository.FindByCondition(p => p.UserId == userId).Include(p => p.Room).Select(p => p.Room).ToListAsync();

            return new Responses<RoomMessageDTO>(true, _mapper.Map<List<RoomMessageDTO>>(rooms));
        }
        public async Task<Responses<RoomDTO>> GetByRoomId(Guid id)
        {
            var rooms = await _roomRepository.GetAll()
                                             .Where(lc => lc.IsDeleted == false && lc.IsActive == true && lc.Id == id)
                                             .Include(a => a.MessageChats)
                                             .Include(a => a.Participants)
                                             .FirstOrDefaultAsync();

            return new Responses<RoomDTO>(true, _mapper.Map<List<RoomDTO>>(rooms));
        }

        public async Task<Response<RoomDTO>> Add(RoomRequest roomRequest, Guid userId)
        {
            //if(roomRequest.Id != null)
            //{
            //    var checkUserRoomExited = await _roomRepository.FindByCondition(r => r.Participants.Where(p => p.UserId == roomRequest.Participant.UserId).Any()).AnyAsync();

            //}
            //if (checkUserRoomExited)
            //{
            //    return new Response<RoomDTO>(false,
            //         "this userId is registered this room name", "422");
            //}
            //else
            //{
            var roomEntity = new Room()
            {
                Participants = new List<Participant>
                    {
                        new Participant
                        {
                            UserId = userId
                        },
                        new Participant
                        {
                            UserId = roomRequest.Participant.UserId,
                        }
                    },
                Name = $"{userId}, {roomRequest.Participant.UserId}",
                Type = ChatType.@private
            };
            //var roomEntity = _mapper.Map<Room>(roomRequest);
            await _roomRepository.CreateAsync(roomEntity);
            await _unitOfWork.SaveChangesAsync();

            //await _participantRepository.CreateAsync(_mapper.Map<Participant>(roomRequest.Participant));
            //await _unitOfWork.SaveChangesAsync();
            var roomDto = _mapper.Map<RoomDTO>(roomEntity);

            return new Response<RoomDTO>(true, roomDto);
        }
    }
}

//public async Task<RoomResponse> Update(Guid id, RoomRequest RoomRequest)
//{
//    try
//    {
//        var rooms = await _roomRepository.GetByIdAsync(id);
//        if (rooms != null)
//        {
//            var result = _roomRepository.Update(_mapper.Map(RoomRequest, rooms));

//            if (!result)
//            {
//                return new RoomResponse
//                {
//                    IsSuccess = false,
//                    Message = "Update fails!"
//                };
//            }

//            await _unitOfWork.SaveChangesAsync();

//            return new RoomResponse
//            {
//                IsSuccess = true,
//                Data = _mapper.Map<RoomDTO>(RoomRequest)
//            };
//        }
//        return new RoomResponse
//        {
//            IsSuccess = false
//        };
//    }
//    catch (Exception ex)
//    {
//        return new RoomResponse
//        {
//            IsSuccess = false,
//            Message = ex.Message.ToString()
//        };
//    }
//}

//public async Task<RoomResponse> Delete(Guid id)
//{
//    try
//    {
//        var rooms = await _roomRepository.GetByIdAsync(id);
//        if (rooms != null)
//        {
//            rooms.IsDeleted = true;
//            rooms.IsActive = false;
//            await _unitOfWork.SaveChangesAsync();
//        }
//        return new RoomResponse
//        {
//            IsSuccess = true
//        };

//    }
//    catch (Exception ex)
//    {
//        return new RoomResponse
//        {
//            IsSuccess = false,
//            Message = ex.Message
//        };
//    }
//}

//public async Task<ListMessageChatResponse> GetMessageByNameRoom(string roomName)
//{
//    try
//    {
//        var room = await _roomRepository.GetAll().Where(r => r.Name == roomName).FirstOrDefaultAsync();
//        var messageChats = await _messageChatRepository.FindByCondition(m => m.RoomId == room.Id).Include(m => m.User).OrderBy(m => m.CreatedAt).ToListAsync();

//        return new ListMessageChatResponse
//        {
//            IsSuccess = true,
//            Data = _mapper.Map<List<MessageChatDTO>>(messageChats)
//        };
//    }
//    catch (Exception ex)
//    {
//        return new ListMessageChatResponse
//        {
//            IsSuccess = false,
//            Message = ex.Message.ToString()
//        };
//    }
//}

//public async Task<RoomNameResponse> GetByNameRoom(string roomName)
//{
//    try
//    {
//        var room = await _roomRepository.FindByCondition(r => r.Name == roomName).FirstOrDefaultAsync();

//        return new RoomNameResponse
//        {
//            IsSuccess = true,
//            Data = _mapper.Map<RoomNameDTO>(room)
//        };
//    }
//    catch (Exception ex)
//    {
//        return new RoomNameResponse
//        {
//            IsSuccess = false,
//            Message = ex.Message.ToString()
//        };
//    }
//}

