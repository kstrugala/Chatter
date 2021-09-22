using Chatter.Core.Entities;
using Chatter.Core.ErrorCodes.V1;
using Chatter.Infrastructure.EF;
using Chatter.Infrastructure.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly ChatterContext _context;

        public PostService(ChatterContext context)
        {
            _context = context;
        }

        public async Task AddPostAsync(string authorsEmail, string title, string content)
        {
            var author = _context.Users.SingleOrDefault(a => a.Email == authorsEmail);

            if (author is null)
                throw new ServiceException(Error.InvalidEmail, $"User with email: {authorsEmail} doesn't exist");

            var post = new Post(title, content, author);
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }
    }
}
