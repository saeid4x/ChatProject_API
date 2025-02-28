using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string  Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public DateTime? DeletedAt { get; set; }    
        public bool IsEdited { get; set; }

        // Foreign key for sender
        public string SenderId   { get; set; }
        public ApplicationUser Sender { get; set; }

        // Foreign key for chat room (if applicable)
        public Guid? ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }

        public ICollection<MessageReaction> MessageReactions { get; set; } = new List<MessageReaction>();
        public ICollection<FileAttachment> FileAttachments { get; set; } = new List<FileAttachment>();


    }
}
