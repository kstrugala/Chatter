using System.Threading.Tasks;

namespace Chatter.Infrastructure.CQRS.Commands
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : class, ICommand;
    }
}
