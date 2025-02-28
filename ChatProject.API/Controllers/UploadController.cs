using ChatProject.Application.Media.Commands;
using ChatProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _environment;

        public UploadController(IMediator mediator , IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _environment = environment;            
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromQuery] Guid messageId, [FromQuery] Guid chatRoomId, [FromQuery] Guid senderId)
        {
            if (file == null || file.Length == 0) return BadRequest("No file uploaded");

            var command = new UploadFileCommand
            {
                File = file,
                MessageId = messageId,
                ChatRoomId = chatRoomId,
                SenderId = senderId
            };

            var result = await _mediator.Send(command);

            return Ok(new { FileId = result, Message = "File uploaded successfully" });
        }



    }
}
