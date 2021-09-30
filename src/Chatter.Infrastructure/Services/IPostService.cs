using Chatter.Infrastructure.Responses.V1.Posts;
using System;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Services
{
    public interface IPostService : IService
    {
        Task<PostDto> GetPostAsync(Guid id);
        Task AddPostAsync(string authorsEmail, string title, string content);
        Task UpdatePostAsync(Guid id, string title, string content);

        Task RemovePostAsync(Guid id);
    }
}
