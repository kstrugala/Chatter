using Chatter.Infrastructure.CQRS.Queries;
using Chatter.Infrastructure.Responses.V1.Posts;
using System;

namespace Chatter.Infrastructure.Queries.V1.Posts
{
    public class GetPostQuery : IQuery<PostDto>
    {
        public Guid Id { get; set; }
    }
}
