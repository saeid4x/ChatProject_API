using ChatProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Application.ChatRooms.Queries
{
    public class GetMessagesQuery:IRequest<List<Message>>
    {
        public Guid ChatRoomId { get; set; }
        public int PageNumber { get; set; } // Page number for pagination
        public int PageSize { get; set; }  // Page size for pagination
    }
}
