using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string  Message { get; set; }
        public bool IsRead  { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // The user to whom the notification is addressed
        public string  UserId { get; set; }
        public ApplicationUser User { get; set; }

        // 
    }
}
