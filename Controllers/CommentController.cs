using Connectify.Interfaces;
using Connectify.Models;
using Connectify.ResponseModel;
using Connectify.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Connectify.Controllers
{
	public class CommentController : Controller
	{
		private readonly IPostsRepository<PostsResponseModel> postsRepository;
		private readonly ICommentRepository<CommentResponseModel> commentRepository;

		public CommentController(IPostsRepository<PostsResponseModel> postsRepository, 
			ICommentRepository<CommentResponseModel> commentRepository) {
			this.postsRepository = postsRepository;
			this.commentRepository = commentRepository;
		}

		[Route("/post/{id}")]
		public async Task<IActionResult> Index(string id)
		{
			var post = await postsRepository.GetById(id);
			var commentsByApi = await commentRepository.GetByPost(id);
			var commentsByDb = await commentRepository.GetByDb(id);
			var allComments = commentsByApi?.Data?.Concat(commentsByDb).OrderBy(c => c.PublishDate);

			post.Comments = allComments?.Count() ?? 0;

			var model = new BaseViewModel {
				SinglePost = post,
				Comments = allComments?.ToList()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CommentModel commentData) {
			var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "USER_NOT_EXIST";
            commentData.CommentId = Guid.NewGuid().ToString();
			commentData.UserId = userId;
			commentData.PublishDate = DateTime.Now;
			if (userId != "USER_NOT_EXIST" && await commentRepository.Create(commentData))
			{
				return Ok(commentData);
			}
			else {
				return NotFound();
			}
		}
	}
}
