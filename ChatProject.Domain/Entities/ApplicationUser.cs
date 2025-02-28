
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Domain.Entities
{
    public class ApplicationUser:IdentityUser
    {
        //public string  DisplayName { get; set; }
        //public string  ProfilePictureUrl { get; set; }
        //public string  StatusMessage { get; set; }


        // Navigation Properties 
        public Profile  Profile { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<ChatRoom> ChatRooms { get; set; }
        public ICollection<UserChatRoom> UserChatRooms { get; set; } = new List<UserChatRoom>();
        public ICollection<MessageReaction> MessageReactions { get; set; } = new List<MessageReaction>();
    }
}
