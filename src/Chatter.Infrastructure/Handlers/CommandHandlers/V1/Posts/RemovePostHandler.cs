using Chatter.Infrastructure.Commands.V1.Posts;
using Chatter.Infrastructure.CQRS.Commands;
using Chatter.Infrastructure.Services;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Handlers.CommandHandlers.V1.Posts
{
    public class RemovePostHandler : ICommandHandler<RemovePostCommand>
    {
        private readonly IPostService _postService;

        public RemovePostHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task HandleAsync(RemovePostCommand command)
            => await _postService.RemovePostAsync(command.Id);
    }
}
