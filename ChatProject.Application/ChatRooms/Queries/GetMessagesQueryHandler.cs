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
    public class GetMessagesQueryHandler:IRequestHandler<GetMessagesQuery,List<Message>>
    {
        private readonly ApplicationDbContext _context;
        public GetMessagesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> Handle(GetMessagesQuery request , CancellationToken cancellationToken)
        {
            return await _context.Messages
                .Where(m =>m.ChatRoomId == request.ChatRoomId)
                .Include(m => m.FileAttachments) // Include file attachments
                .OrderByDescending(m =>m.CreatedAt)
                .Skip((request.PageNumber -1) * request.PageSize) // Pagination logic
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
