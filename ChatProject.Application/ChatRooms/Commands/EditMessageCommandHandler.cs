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
    public class EditMessageCommandHandler:IRequestHandler<EditMessageCommand,Message>
    {
        private readonly ApplicationDbContext _context;

        public EditMessageCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Message> Handle(EditMessageCommand request , CancellationToken cancellationToken)
        {
            var message = await _context.Messages.FindAsync(request.MessageId);
            if(message == null)
            {
                throw new Exception("Message not found");
            }

            message.Content = request.NewContent;
            message.EditedAt = DateTime.UtcNow;
            message.IsEdited = true;

            _context.Messages.Update(message);
            await _context.SaveChangesAsync(cancellationToken);

            return message;
        }

    }
}
