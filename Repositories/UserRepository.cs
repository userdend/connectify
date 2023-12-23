using Microsoft.EntityFrameworkCore;
using Connectify.Data;
using Connectify.Interfaces;
using Connectify.ResponseModel;
using Connectify.ViewModels;
using Connectify.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Connectify.Repositories
{
    public class UserRepository : IUserRepository<UserResponseModel>
    {
        private readonly HttpClient httpClient;
        private readonly AppDbContext appDbContext;

        public UserRepository(HttpClient httpClient, AppDbContext appDbContext) {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://dummyapi.io/data/v1/");
            this.httpClient.DefaultRequestHeaders.Add("app-id", "64ff4f8b6beeb80cbbbb4909");

            this.appDbContext = appDbContext;
        }

        public async Task Authentication(HttpContext httpContext, ProfileViewModel userInfo)
        {
            List<Claim> claims = new() {
                new Claim(ClaimTypes.NameIdentifier, userInfo.Id),
                new Claim(ClaimTypes.Name, userInfo.FirstName),
                new Claim(ClaimTypes.Surname, userInfo.LastName)
            };

            ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new() {
                AllowRefresh = true,
            };

            await httpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    properties
                );
        }

        public async Task<bool> Create(RegisterViewModel registerInfo)
        {
            try {
                var findUser = await appDbContext.UserInfo.FirstOrDefaultAsync(uInfo => uInfo.Email == registerInfo.Email);

                if (findUser == null)
                {
                    Guid userId = Guid.NewGuid();
                    string hashPassword = BCrypt.Net.BCrypt.HashPassword(registerInfo.Password);

                    var userInfo = new UserInfoModel
                    {
                        UserId = userId.ToString(),
                        FirstName = registerInfo.FirstName,
                        LastName = registerInfo.LastName,
                        Email = registerInfo.Email,
                        Timezone = TimeZoneInfo.Local.ToString(),
                        RegisterDate = DateTime.Now,
                    };

                    var userPassword = new UserPasswordModel
                    {
                        UserId = userId.ToString(),
                        Password = hashPassword,
                    };

                    await appDbContext.UserInfo.AddAsync(userInfo);
                    await appDbContext.UserPassword.AddAsync(userPassword);
                    await appDbContext.SaveChangesAsync();

                    return true;
                }

                return false;
            }

            catch (DbUpdateException) {
                return false;
            }
        }

        public async Task Exit(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<ProfileViewModel> Find(LoginViewModel loginInfo)
        {
            try {
                var userInfo = await appDbContext.UserInfo.SingleOrDefaultAsync(uInfo => uInfo.Email == loginInfo.Email);
                if (userInfo != null)
                {
                    var userPassword = await appDbContext.UserPassword.SingleOrDefaultAsync(uPassword => uPassword.UserId == userInfo.UserId);
                    if (userPassword != null)
                    {
                        var validPassword = BCrypt.Net.BCrypt.Verify(loginInfo.Password, userPassword.Password);
                        if (validPassword)
                        {
                            return new ProfileViewModel
                            {
                                Id = userInfo.UserId,
                                FirstName = userInfo.FirstName,
                                LastName = userInfo.LastName,
                            };
                        }
                    }
                }

                return new();
            }

            catch (DbUpdateException) {
                return new();
            }
        }

        public async Task<ProfileViewModel> GetById(string id)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"user/{id}");

            if (httpResponseMessage.IsSuccessStatusCode) {
                return await httpResponseMessage.Content.ReadAsAsync<ProfileViewModel>();
            }

            return new();
        }

        public async Task<UserResponseModel> GetUser()
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("user?limit=5");

            if (httpResponseMessage.IsSuccessStatusCode) {
                return await httpResponseMessage.Content.ReadAsAsync<UserResponseModel>();
            }

            return new();
        }
    }
}
