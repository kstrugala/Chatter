using Chatter.Infrastructure.CQRS.Commands;
using System;
using System.Text.Json.Serialization;

namespace Chatter.Infrastructure.Commands.V1.Posts
{
    public class UpdatePostCommand : ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
