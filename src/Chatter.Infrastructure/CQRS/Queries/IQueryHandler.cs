using System.Threading.Tasks;

namespace Chatter.Infrastructure.CQRS.Queries
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : class, IQuery<TResult>
    {
        Task<TResult> HandleAsync(IQuery query);
    }
}
