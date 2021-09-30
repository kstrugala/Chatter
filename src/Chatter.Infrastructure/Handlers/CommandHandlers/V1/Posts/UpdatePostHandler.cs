using Chatter.Infrastructure.Commands.V1.Posts;
using Chatter.Infrastructure.CQRS.Commands;
using Chatter.Infrastructure.Services;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Handlers.CommandHandlers.V1.Posts
{
    public class UpdatePostHandler : ICommandHandler<UpdatePostCommand>
    {
        private readonly IPostService _postService;

        public UpdatePostHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task HandleAsync(UpdatePostCommand command)
            => await _postService.UpdatePostAsync(command.Id, command.Title, command.Content);
    }
}
