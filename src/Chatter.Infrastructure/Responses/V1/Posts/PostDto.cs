using System;

namespace Chatter.Infrastructure.Responses.V1.Posts
{
    public class PostDto
    {
        public Guid Id { get; set;  }

        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
