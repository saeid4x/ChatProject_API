using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Application.ChatRooms.Commands
{
    public class MarkMessageAsDeliveredCommand:IRequest<bool>
    {
        public Guid MessageId { get; set; }
        public string  UserId { get; set; }
    }
}
