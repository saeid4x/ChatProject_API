using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Application.ChatRooms.Commands
{
    public class SendMessageWithAttachmentCommand:IRequest<MessageDto>
    {
        
            public Guid ChatRoomId { get; set; }
            public string SenderId { get; set; }
            public string Content { get; set; }
            public IFormFile Attachment { get; set; } // Optional file attachment
         
    }
}
