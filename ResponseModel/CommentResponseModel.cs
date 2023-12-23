using Connectify.ViewModels;

namespace Connectify.ResponseModel
{
	public class CommentResponseModel
	{
		public List<CommentViewModel>? Data { get; set; } 
		public int Total { get; set; }
	}
}
