namespace Connectify.ViewModels
{
    public class PostsViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int Likes { get; set; }
        public List<string>? Tags { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        public UserViewModel? Owner { get; set; }
        public int Comments { get; set; }
    }
}
