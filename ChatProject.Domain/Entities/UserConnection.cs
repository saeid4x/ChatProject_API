using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Domain.Entities
{
    public class UserConnection
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string  UserId { get; set; }
        public ApplicationUser User { get; set; }

        // The SignalR connection identifier
        public string  ConnectionId { get; set; }
        public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;
    }
}
