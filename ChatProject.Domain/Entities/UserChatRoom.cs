using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Domain.Entities
{
    public  class UserChatRoom
    {
        public bool IsAdmin { get; set; }   

        public string UserId  { get; set; }
        public ApplicationUser User { get; set; }

        public Guid ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}
