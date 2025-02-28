using ChatProject.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using ChatProject.Domain.Entities;
using ChatProject.Domain.Common.Interfaces;


namespace ChatProject.Application.Media.Commands
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Guid>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;

        public UploadFileCommandHandler(ApplicationDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        public async Task<Guid> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            string fileUrl = await _fileStorageService.SaveFileAsync(request.File);

            var fileAttachment = new FileAttachment
            {
                FileUrl = fileUrl,
                FileName = request.File.FileName,
                ContentType = request.File.ContentType,
                Filesize = request.File.Length,
                UploadedAt = DateTime.UtcNow,
                MessageId = request.MessageId,
                //ChatRoomId = request.ChatRoomId,
                //SenderId = request.SenderId
            };

            _context.FileAttachments.Add(fileAttachment);
            await _context.SaveChangesAsync(cancellationToken);

            return fileAttachment.Id;
        }

    }

    }
