using ChatProject.Application.Interfaces;
using ChatProject.Infrastructure.Persistence;
using MediatR;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Application.ChatRooms.Commands
{
    public class DeleteMessageCommandHandler :IRequestHandler<DeleteMessageCommand , bool>
    {
        private readonly ApplicationDbContext _context;
        //  private readonly IHubContext<ChatHub> _hubContext; // Inject SignalR Hub
        private readonly IChatNotificationService _chatNotificationService; // Use abstraction

        public DeleteMessageCommandHandler(ApplicationDbContext context, IChatNotificationService chatNotificationService)
        {
            _context = context;
            _chatNotificationService= chatNotificationService;
        }

        public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _context.Messages.FindAsync(request.MessageId);
            if (message == null) return false;

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync(cancellationToken);

            if (!message.ChatRoomId.HasValue)
            {
                return false; // Prevent calling the notification with a null ChatRoomId
            }


            // Notify SignalR through the service
            await _chatNotificationService.NotifyMessageDeleted(message.ChatRoomId.Value, request.MessageId);

            return true;
        }
    }
}
