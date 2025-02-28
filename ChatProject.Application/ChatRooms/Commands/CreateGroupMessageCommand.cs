using ChatProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Application.ChatRooms.Commands
{
    public class CreateGroupMessageCommand:IRequest<Message>
    {
        public Guid ChatRoomId { get; set; }
        public string SenderId { get; set; }
        public string  Content { get; set; }

    }
}
