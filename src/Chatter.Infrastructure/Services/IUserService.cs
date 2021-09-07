using System.Threading.Tasks;

namespace Chatter.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task SignUp(string email, string password, string firstName, string lastName);
    }
}
