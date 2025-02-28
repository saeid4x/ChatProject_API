using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Domain.Entities
{
    public class PinnedMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime PinnedAt { get; set; } = DateTime.UtcNow;

        // Reference to the pinned message
        public Guid MessageId { get; set; }
        public Message Message { get; set; }


        // The chat room where the message is pinned
        public Guid ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }

     

    }
}
