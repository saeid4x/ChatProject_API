using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Application.Interfaces
{
    public interface IChatNotificationService
    {
        Task NotifyMessageDeleted(Guid chatRoomId, Guid messageId);
    }
}
