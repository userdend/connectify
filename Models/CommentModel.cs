namespace Connectify.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string CommentId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string PostId { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
    }
}
