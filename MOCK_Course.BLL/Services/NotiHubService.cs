using Microsoft.AspNetCore.SignalR;

namespace Course.BLL.Services
{
    public class NotiHubService : Hub, INotiHubService
    {
        //    public async Task SendMessage(string userId, string text)
        //=> await Clients.User(userId).SendAsync("ReceiveMessage", text);

        //    public string GetConnectionId() => Context.ConnectionId;

        //private readonly IDictionary<string, string> _connections;
        //private readonly UserManager<AppUser> _userManager;
        //private readonly IMessageChatService _messageChatService;
        //private readonly IRoomService _roomService;

        //public NotiHubService(IDictionary<string, string> connections, IMessageChatService messageChatService, IRoomService roomService)
        //{
        //    _connections = connections;
        //    _messageChatService = messageChatService;
        //    _roomService = roomService;
        //}

        //public override Task OnDisconnectedAsync(Exception exception)
        //{
        //    if (_connections.TryGetValue(Context.ConnectionId, out string userId))
        //    {
        //        _connections.Remove(Context.ConnectionId);
        //        //Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", userConnection.User, $"{userConnection.User} has left");
        //        //SendUsersConnected(userConnection.Room);
        //    }
        //    return base.OnDisconnectedAsync(exception);
        //}
    }
}
