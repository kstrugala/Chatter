namespace Chatter.Infrastructure.CQRS.Queries
{
    public interface IQuery
    {
    }

    public interface IQuery<T> : IQuery
    {
    }
}
