namespace ChatProject.API.DTOs
{
    public class SendMessageWithAttachmentRequestDto
    {
        public Guid ChatRoomId { get; set; }
        public string SenderId { get; set; }
        public string Content { get; set; }
        public IFormFile Attachment { get; set; }
    }
}
