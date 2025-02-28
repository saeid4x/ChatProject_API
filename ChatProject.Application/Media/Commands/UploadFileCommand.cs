using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Application.Media.Commands
{
    public class UploadFileCommand:IRequest<Guid>
    {
        public IFormFile File { get; set; }  // Uploaded file
        public Guid MessageId { get; set; }  // Message to attach the file
        public Guid ChatRoomId { get; set; } // Chat room where the file was uploaded
        public Guid SenderId { get; set; }   // User who uploaded the file
    }
}
