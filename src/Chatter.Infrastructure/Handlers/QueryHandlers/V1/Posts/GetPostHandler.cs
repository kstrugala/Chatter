using Chatter.Infrastructure.CQRS.Queries;
using Chatter.Infrastructure.Queries.V1.Posts;
using Chatter.Infrastructure.Responses.V1.Posts;
using Chatter.Infrastructure.Services;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Handlers.QueryHandlers.V1.Posts
{
    public class GetPostHandler : IQueryHandler<GetPostQuery, PostDto>
    {
        private readonly IPostService _postService;

        public GetPostHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<PostDto> HandleAsync(GetPostQuery query)
            => await _postService.GetPostAsync(query.Id);
    }
}
