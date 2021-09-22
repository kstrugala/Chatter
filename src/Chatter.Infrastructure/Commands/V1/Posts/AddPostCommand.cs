using Chatter.Infrastructure.CQRS.Commands;
using System.Text.Json.Serialization;

namespace Chatter.Infrastructure.Commands.V1.Posts
{
    public class AddPostCommand : ICommand
    {
        [JsonIgnore]
        public string AuthorsEmail { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
