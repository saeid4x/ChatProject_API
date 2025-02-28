using ChatProject.Domain.Common.Interfaces;
using ChatProject.Domain.Entities;
using ChatProject.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Application.ChatRooms.Commands
{
    public  class SendMessageWithAttachmentCommandHandler : IRequestHandler<SendMessageWithAttachmentCommand, MessageDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;

        public SendMessageWithAttachmentCommandHandler(ApplicationDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }


        public async Task<MessageDto> Handle(SendMessageWithAttachmentCommand request, CancellationToken cancellationToken)
        {
            // Create the Message record
            var message = new Message
            {
                Id = Guid.NewGuid(),
                ChatRoomId = request.ChatRoomId,
                SenderId = request.SenderId,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow,
                IsEdited = false
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync(cancellationToken);

            string attachmentUrl = null;
            // If an attachment is provided, upload it and create a FileAttachment record
            if (request.Attachment != null)
            {
                // Upload file using the file storage service
                attachmentUrl = await _fileStorageService.SaveFileAsync(request.Attachment);

                var fileAttachment = new FileAttachment
                {
                    Id = Guid.NewGuid(),
                    FileUrl = attachmentUrl,
                    FileName = request.Attachment.FileName,
                    ContentType = request.Attachment.ContentType,
                    Filesize = request.Attachment.Length,
                    UploadedAt = DateTime.UtcNow,
                    MessageId = message.Id  // Link the file to the message
                };

                _context.FileAttachments.Add(fileAttachment);
                await _context.SaveChangesAsync(cancellationToken);
            }

            // Build and return a DTO with message details (including attachment URL if any)
            var result = new MessageDto
            {
                Id = message.Id,
                ChatRoomId = message.ChatRoomId ,
                SenderId = message.SenderId,
                Content = message.Content,
                CreatedAt = message.CreatedAt,
                AttachmentUrl = attachmentUrl
            };

            return result;
        }
    }
}
