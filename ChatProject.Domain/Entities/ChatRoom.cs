using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Domain.Entities
{
    public class ChatRoom
    {
        public Guid Id { get; set; }
        public string  Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; } = null;

        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<Message> Messages { get; set; }
        
        public ICollection<UserChatRoom> UserChatRooms { get; set; } = new List<UserChatRoom>();
    }
}
