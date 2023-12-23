using Connectify.Models;
using Connectify.ViewModels;

namespace Connectify.Interfaces
{
    public interface ICommentRepository<TEntity>
    {
        Task<TEntity> GetByPost(string id);
        Task<List<CommentViewModel>> GetByDb(string id);
        Task<bool> Create(CommentModel commentData);
    }
}
