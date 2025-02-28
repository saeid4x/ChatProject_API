using ChatProject.Domain.Entities;
using ChatProject.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Application.ChatRooms.Commands
{
    public class CreateChatRoomCommandHandler:IRequestHandler<CreateChatRoomCommand, ChatRoom>
    {
        private readonly ApplicationDbContext _context;

        public CreateChatRoomCommandHandler(ApplicationDbContext context)
        {
            _context = context; 
        }


        public async Task<ChatRoom> Handle(CreateChatRoomCommand request, CancellationToken cancellationToken)
        {
            var chatRoom = new ChatRoom
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedAt = DateTime.UtcNow
            };

            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync(cancellationToken);
            return chatRoom;
        }
    }
}
