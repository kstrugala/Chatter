using AutoMapper;
using Chatter.Infrastructure.CQRS.Queries;
using Chatter.Infrastructure.Queries.V1.Users;
using Chatter.Infrastructure.Responses.V1.Users;
using Chatter.Infrastructure.Services;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Handlers.QueryHandlers.V1.Users
{
    public class SignInHandler : IQueryHandler<SignInQuery, JsonWebTokenDto>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper; 

        public SignInHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<JsonWebTokenDto> HandleAsync(SignInQuery query)
        {
            var token = await _userService.SignInAsync(query.Email, query.Password);
            return _mapper.Map<JsonWebTokenDto>(token);
        }
    }
}
