using Autofac;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.CQRS.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task SendAsync<T>(T command) where T : class, ICommand
            => await _context.Resolve<ICommandHandler<T>>().HandleAsync(command);
    }
}
