using AutoMapper;
using Chatter.Core.Entities;
using Chatter.Core.ErrorCodes.V1;
using Chatter.Infrastructure.EF;
using Chatter.Infrastructure.Exceptions;
using Chatter.Infrastructure.Responses.V1.Posts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly ChatterContext _context;
        private readonly IMapper _mapper;

        public PostService(ChatterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostDto> GetPostAsync(Guid id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(p => p.UniqueId == id);

            return  _mapper.Map<PostDto>(post);
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
