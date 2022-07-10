using Course.DAL.Models;
using Entities.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public class ChatHubService : Hub, IChatHubService
    {

        public async Task SendMessage(Guid userId, string text)
    => await Clients.All.SendAsync("ReceiveMessage", userId, text);

        public async Task SendMessageToCaller(Guid userId, string text)
            => await Clients.Caller.SendAsync("ReceiveMessage", userId, text);

        public async Task SendMessageToGroup(Guid userId, string text)
            => await Clients.Group("SignalR Users").SendAsync("ReceiveMessage", userId, text);

        public Task SendPrivateMessage(Guid userId, string message)
            => Clients.User(userId.ToString()).SendAsync("ReceiveMessage", message);

        public async Task AddToGroup(string groupName, Guid userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{userId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName, Guid userId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{userId} has left the group {groupName}.");
        }


        private readonly IDictionary<string, Guid> _connections;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMessageChatService _messageChatService;
        private readonly IRoomService _roomService;

        public ChatHubService(IDictionary<string, Guid> connections, IMessageChatService messageChatService, IRoomService roomService)
        {
            _connections = connections;
            _messageChatService = messageChatService;
            _roomService = roomService;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out Guid userId))
            {
                _connections.Remove(Context.ConnectionId);
                //Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", userConnection.User, $"{userConnection.User} has left");
                //SendUsersConnected(userConnection.Room);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task JoinRoom(JoinGroupRequest joinGroupRequest)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, joinGroupRequest.RoomId.ToString());

            _connections[Context.ConnectionId] = joinGroupRequest.UserId;

            var getRoom = await _roomService.GetByRoomId(joinGroupRequest.RoomId);
            await Clients.Caller.SendAsync("ReceiveHistoryMessage", getRoom.Data.Select(r => r.MessageChats));

            //await SendUsersConnected(userConnection.Room);
            //var a = new RoomRequest()
            //{
            //    Name = userConnection.Room,
            //    Participant = new ParticipantRequest()
            //    {
            //        UserId = Guid.Parse("D2F5074D-5F83-491F-8AB0-7E6C2CAEFC2D")
            //    }
            //};

            //await _roomService.Add(a);
        }

        //public async Task SendMessage(string message)
        //{
        //    if (_connections.TryGetValue(Context.ConnectionId, out UserConnectionRequest1 userConnection1))
        //    {
        //        await Clients.Group(userConnection1.Room).SendAsync("ReceiveMessage", userConnection1.User, message);


        //        var getRoom = await _roomService.GetByNameRoom(userConnection1.Room);
        //        var userConnection = new MessageChatRequest()
        //        {
        //            UserId = userConnection1.User,
        //            RoomId = getRoom.Data.Id,
        //            Text = message
        //        };

        //        await _messageChatService.Add(userConnection);
        //    }
        //}

        //public Task SendUsersConnected(string room)
        //{
        //    var users = _connections.Values
        //        .Where(c => c.Room == room)
        //        .Select(c => c.User);

        //    return Clients.Group(room).SendAsync("UsersInRoom", users);
        //}
    }
}
