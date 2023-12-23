using Connectify.Interfaces;
using Connectify.ResponseModel;
using Connectify.ViewModels;

namespace Connectify.Repositories
{
    public class PostsRepository : IPostsRepository<PostsResponseModel>
    {
        private readonly HttpClient httpClient;

        public PostsRepository(HttpClient httpClient) {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://dummyapi.io/data/v1/");
            this.httpClient.DefaultRequestHeaders.Add("app-id", "64ff4f8b6beeb80cbbbb4909");
        }

        public async Task<PostsResponseModel> GetAll()
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("post?limit=5");

            if (httpResponseMessage.IsSuccessStatusCode) {
                return await httpResponseMessage.Content.ReadAsAsync<PostsResponseModel>();
            }

            return new();
        }

        public async Task<PostsViewModel> GetById(string id)
        {
			HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"post/{id}");

			if (httpResponseMessage.IsSuccessStatusCode)
			{
				return await httpResponseMessage.Content.ReadAsAsync<PostsViewModel>();
			}

            return new();
        }

        public async Task<PostsResponseModel> GetByTags(string tag)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"tag/{tag}/post?limit=5");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsAsync<PostsResponseModel>();
            }

            return new();
        }

        public async Task<PostsResponseModel> GetByUser(string id)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"user/{id}/post?limit=5");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsAsync<PostsResponseModel>();
            }

            return new();
        }
    }
}
