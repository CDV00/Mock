using Entities.Requests;
using System;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public interface IChatHubService
    {
        Task AddToGroup(string groupName, Guid userId);
        Task JoinRoom(JoinGroupRequest joinGroupRequest);
        Task OnDisconnectedAsync(Exception exception);
        Task RemoveFromGroup(string groupName, Guid userId);
        //Task SendMessage(string message);
        //Task SendMessage(Guid userId, string text);
        Task SendMessageToCaller(Guid userId, string text);
        Task SendMessageToGroup(Guid userId, string text);
        Task SendPrivateMessage(Guid userId, string message);
        //Task SendUsersConnecthttp://animevietsub.tv/ed(string room);
    }
}