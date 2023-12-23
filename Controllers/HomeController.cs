using Connectify.Interfaces;
using Connectify.Models;
using Connectify.ResponseModel;
using Connectify.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Connectify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository<UserResponseModel> userRepository;
        private readonly IPostsRepository<PostsResponseModel> postsRepository;
        private readonly ICommentRepository<CommentResponseModel> commentRepository;

        public HomeController(ILogger<HomeController> logger,
            IUserRepository<UserResponseModel> userRepository,
            IPostsRepository<PostsResponseModel> postsRepository,
            ICommentRepository<CommentResponseModel> commentRepository
            )
        {
            _logger = logger;
            this.userRepository = userRepository;
            this.postsRepository = postsRepository;
            this.commentRepository = commentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var postsByApi = await postsRepository.GetAll();
            var allPosts = postsByApi?.Data?.ToList();

            if (allPosts != null) {
                foreach (var post in allPosts) {
                    var t_commentsByApi = await commentRepository.GetByPost(post.Id);
                    var t_commentsByDb = await commentRepository.GetByDb(post.Id);
                    post.Comments = t_commentsByApi.Total + t_commentsByDb.Count;
                }
            }

            var users = await userRepository.GetUser();
            var model = new BaseViewModel {
                Users = users.Data,
                Posts = allPosts
            };

            TempData["Authorized"] = HttpContext.User.Identity?.IsAuthenticated;

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}