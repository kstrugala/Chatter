using Chatter.Core.Entities;
using Chatter.Infrastructure.Commands.V1.Posts;
using Chatter.Infrastructure.CQRS.Commands;
using Chatter.Infrastructure.Services;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Handlers.CommandHandlers.V1.Posts
{
    public class AddPostHandler : ICommandHandler<AddPostCommand>
    {
        private readonly IPostService _postService;

        public AddPostHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task HandleAsync(AddPostCommand command)
            => await _postService.AddPostAsync(command.AuthorsEmail, command.Title, command.Content);
    }
}
