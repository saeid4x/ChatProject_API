using ChatProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Application.ChatRooms.Queries
{
    public  class GetChatRoomsQuery : IRequest<List<ChatRoom>>
    {
    }
}
