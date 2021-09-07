using Chatter.Infrastructure.CQRS.Commands;
using Chatter.Infrastructure.CQRS.Queries;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.CQRS.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<T>(T command) where T : class, ICommand;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
        Task<TResult> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>;
    }
}
