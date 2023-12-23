using Connectify.Interfaces;
using Connectify.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostsRepository<PostsResponseModel> postsRepository;

        public PostsController(IPostsRepository<PostsResponseModel> postsRepository) {
            this.postsRepository = postsRepository;
        }

        public IActionResult Create() {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Tags(string tag) {
            if (tag != null) {
                var result = await postsRepository.GetByTags(tag);
                ViewBag.Tags = tag;
                return View(result.Data);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
