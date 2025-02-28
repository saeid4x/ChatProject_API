using ChatProject.API.Hubs;
using ChatProject.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

namespace ChatProject.API.Services
{
    public class ChatNotificationService : IChatNotificationService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatNotificationService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }


        public async Task NotifyMessageDeleted(Guid chatRoomId, Guid messageId)
        {
            await _hubContext.Clients.Group(chatRoomId.ToString())
                    .SendAsync("MessageDeleted", messageId);
               
        }
    }
}
