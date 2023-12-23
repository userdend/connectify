using Connectify.ViewModels;

namespace Connectify.Interfaces
{
    public interface IUserRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetUser();
        Task<ProfileViewModel> GetById(string id);
        Task<bool> Create(RegisterViewModel registerInfo);
        Task<ProfileViewModel> Find(LoginViewModel loginInfo);
        Task Authentication(HttpContext httpContext, ProfileViewModel userInfo);
        Task Exit(HttpContext httpContext);
    }
}
