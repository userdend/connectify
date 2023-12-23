namespace Connectify.ViewModels
{
    public class CommentViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public UserViewModel? Owner { get; set; }
        public string Post { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
    }
}
