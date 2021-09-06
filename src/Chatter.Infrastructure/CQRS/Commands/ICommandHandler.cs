using System.Threading.Tasks;

namespace Chatter.Infrastructure.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
