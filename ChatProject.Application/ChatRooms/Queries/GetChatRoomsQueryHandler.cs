using ChatProject.Domain.Entities;
using ChatProject.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Application.ChatRooms.Queries
{
    public class GetChatRoomsQueryHandler:IRequestHandler<GetChatRoomsQuery , List<ChatRoom>>
    {
        private ApplicationDbContext _context;

        public GetChatRoomsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<List<ChatRoom>> Handle(GetChatRoomsQuery request , CancellationToken cancellationToken)
        {
            return await _context.ChatRooms.ToListAsync(cancellationToken);
        }
    }
}
