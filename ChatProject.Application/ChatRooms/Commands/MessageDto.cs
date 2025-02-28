using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Application.ChatRooms.Commands
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid? ChatRoomId { get; set; }
        public string SenderId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        // Optionally, you can include attachment info if needed
        public string AttachmentUrl { get; set; }
    }
}
