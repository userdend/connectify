using Connectify.Data;
using Connectify.Interfaces;
using Connectify.Models;
using Connectify.ResponseModel;
using Connectify.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Connectify.Repositories
{
    public class CommentRepository : ICommentRepository<CommentResponseModel>
    {
        private readonly HttpClient httpClient;
        private readonly AppDbContext appDbContext;

        public CommentRepository(HttpClient httpClient, AppDbContext appDbContext) {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://dummyapi.io/data/v1/");
			this.httpClient.DefaultRequestHeaders.Add("app-id", "64ff4f8b6beeb80cbbbb4909");

            this.appDbContext = appDbContext;
		}

        public async Task<bool> Create(CommentModel commentData)
        {
            try {
                await appDbContext.Comment.AddAsync(commentData);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<List<CommentViewModel>> GetByDb(string id)
        {
            try {
                var query = from comment in appDbContext.Comment
                            join userInfo in appDbContext.UserInfo on comment.UserId equals userInfo.UserId
                            where comment.PostId == id
                            select new CommentViewModel
                            {
                                Id = comment.CommentId,
                                Message = comment.Message,
                                Owner = new UserViewModel() {
                                    Id = userInfo.UserId,
                                    FirstName = userInfo.FirstName,
                                    LastName = userInfo.LastName,
                                    Picture = userInfo.Picture
                                },
                                Post = comment.PostId,
                                PublishDate = comment.PublishDate
                            };
                return await query.ToListAsync();
            }
            catch (DbUpdateException) {
                return new();
            }
        }

        public async Task<CommentResponseModel> GetByPost(string id)
        {
			HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"post/{id}/comment");

			if (httpResponseMessage.IsSuccessStatusCode)
			{
				return await httpResponseMessage.Content.ReadAsAsync<CommentResponseModel>();
			}

            return new();
		}
    }
}
