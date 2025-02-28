using ChatProject.API.DTOs;
using ChatProject.Application.ChatRooms.Commands;
using ChatProject.Application.ChatRooms.Queries;
using ChatProject.Domain.Entities;
using ChatProject.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatRoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;
        public ChatRoomsController(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;  
            _mediator = mediator;
        }

        // GET: api/chatrooms
        [HttpGet]
        public async Task<ActionResult<List<ChatRoom>>> GetChatRrooms()
        {
            var rooms = await _mediator.Send(new GetChatRoomsQuery());
            return Ok(rooms);
            //var rooms = await _context.ChatRooms.ToListAsync();
            //return Ok(rooms);
        }

        // POST: api/chatrooms
        [HttpPost]
        public async Task<ActionResult<ChatRoom>> CreateChatRoom([FromBody] CreateChatRoomCommand command)
        {
            var chatRoom = await _mediator.Send(command);
            return Ok(chatRoom); 
        }


        [HttpGet("{chatRoomId}/messages")]
        public async Task<ActionResult<List<Message>>> GetMessages(Guid chatRoomId , [FromQuery] int pageNumber=1, [FromQuery] int pageSize = 10)
        {
            var query = new GetMessagesQuery
            {
                ChatRoomId = chatRoomId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var messages = await _mediator.Send(query);
            return Ok(messages);
        }


        [HttpDelete("message/{messageId}")]
        public async Task<IActionResult> DeleteMessage(Guid messageId)
        {
            var result = await _mediator.Send(new DeleteMessageCommand { MessageId = messageId });

            if (!result)
                return NotFound();

            return NoContent();
        }


        [HttpPost("send")]
        public async Task<IActionResult> SendMessageWithAttachment([FromForm] SendMessageWithAttachmentRequestDto dto)
        {
            var command = new SendMessageWithAttachmentCommand
            {
                ChatRoomId = dto.ChatRoomId,
                SenderId = dto.SenderId,
                Content = dto.Content,
                Attachment = dto.Attachment
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
