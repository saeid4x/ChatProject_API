using ChatProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Application.ChatRooms.Commands
{
    public class EditMessageCommand:IRequest<Message>
    {
        public Guid MessageId { get; set; }
        public string  NewContent { get; set; }

    }
}
