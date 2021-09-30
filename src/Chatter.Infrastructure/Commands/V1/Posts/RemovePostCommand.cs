using Chatter.Infrastructure.CQRS.Commands;
using System;

namespace Chatter.Infrastructure.Commands.V1.Posts
{
    public class RemovePostCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
