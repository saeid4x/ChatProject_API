using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatProject.Domain.Entities
{
    public class FileAttachment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string  FileUrl { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long  Filesize { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
        public Guid MessageId { get; set; }

        [JsonIgnore] // Prevents circular reference
        public Message Message { get; set; }


    }
}
