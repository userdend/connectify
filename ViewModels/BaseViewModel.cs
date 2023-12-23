using System.Security.Claims;

namespace Connectify.ViewModels
{
	public class BaseViewModel
	{
		public IEnumerable<UserViewModel>? Users { get; set; }
		public IEnumerable<PostsViewModel>? Posts { get; set; }
		public List<CommentViewModel>? Comments { get; set; }
		public PostsViewModel? SinglePost { get; set; }
		public ProfileViewModel? Profile { get; set; }
		public ClaimsPrincipal? ClaimsPrincipal { get; set; }
	}
}
