using Connectify.Interfaces;
using Connectify.ResponseModel;
using Connectify.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserRepository<UserResponseModel> userRepository;
		private readonly IPostsRepository<PostsResponseModel> postsRepository;

		public UserController(IUserRepository<UserResponseModel> userRepository,
			IPostsRepository<PostsResponseModel> postsRepository) {
			this.userRepository = userRepository;
			this.postsRepository = postsRepository;
		}

		[Route("/{firstName}-{lastName}-{id}")]
		public async Task<IActionResult> Profile(string id) {
			try {
                var user = await userRepository.GetById(id);
                var posts = await postsRepository.GetByUser(id);
                var model = new BaseViewModel
                {
                    Profile = user,
                    Posts = posts.Data
                };
				return View(model);
            }
			catch (Exception) {
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginInfo)
        {
            if (ModelState.IsValid) {
                var userInfo = await userRepository.Find(loginInfo);
                if (userInfo != null) {
                    await userRepository.Authentication(HttpContext, userInfo);
                    return RedirectToAction("Index", "Home");
                }
                TempData["Result"] = "Login failed. Incorrect email or password.";
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerInfo)
        {
            if (ModelState.IsValid) {
                bool createUser = await userRepository.Create(registerInfo);
                TempData["Result"] = createUser ? "Successfully registered." : "Email already in used.";
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await userRepository.Exit(HttpContext);
            return RedirectToAction("Index", "Home");
        }
    }
}
