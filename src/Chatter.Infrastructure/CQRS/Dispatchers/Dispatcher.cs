using Chatter.Infrastructure.CQRS.Commands;
using Chatter.Infrastructure.CQRS.Queries;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.CQRS.Dispatchers
{
    public class Dispatcher : IDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public Dispatcher(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public async Task SendAsync<T>(T command) where T : class, ICommand
            => await _commandDispatcher.SendAsync<T>(command);

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
            => await _queryDispatcher.QueryAsync<TResult>(query);

        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>
            => await _queryDispatcher.QueryAsync<TQuery, TResult>(query);
    }
}
