using Connectify.ViewModels;

namespace Connectify.Interfaces
{
    public interface IPostsRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAll();
        Task<TEntity> GetByTags(string tag);
        Task<PostsViewModel> GetById(string id);
        Task<TEntity> GetByUser(string id);
    }
}
