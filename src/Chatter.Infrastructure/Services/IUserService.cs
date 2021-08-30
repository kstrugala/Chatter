using System.Threading.Tasks;

namespace Chatter.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task SignUp(string email, string password, string firsName, string lastName);
    }
}
