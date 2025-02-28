using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Domain.Entities
{
    public class MessageReaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string  ReactionType { get; set; }
        public DateTime ReactedAt { get; set; } = DateTime.UtcNow;
        public Guid MessageId { get; set; }
        public Message Message { get; set; }

        public string  UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
