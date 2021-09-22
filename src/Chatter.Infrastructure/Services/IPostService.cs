using Chatter.Core.Entities;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Services
{
    public interface IPostService : IService
    {
        Task AddPostAsync(string authorsEmail, string title, string content);
    }
}
