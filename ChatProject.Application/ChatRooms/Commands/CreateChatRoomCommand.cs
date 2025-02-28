using ChatProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Application.ChatRooms.Commands
{
    public class CreateChatRoomCommand:IRequest<ChatRoom>
    {
        public string  Name { get; set; }
    }
}
