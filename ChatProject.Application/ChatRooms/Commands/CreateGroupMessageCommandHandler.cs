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
    public class CreateGroupMessageCommandHandler:IRequestHandler<CreateGroupMessageCommand , Message>
    {
        private readonly ApplicationDbContext _context;

        public CreateGroupMessageCommandHandler(ApplicationDbContext context)
        {
            _context = context;            
        }

        public async Task<Message> Handle(CreateGroupMessageCommand request , CancellationToken cancellationToken)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                ChatRoomId = request.ChatRoomId,
                SenderId = request.SenderId,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow,
                IsEdited = false
            };

            _context.Messages.Add(message); 
            await _context.SaveChangesAsync(cancellationToken);
            return message;
        }
    }
}
