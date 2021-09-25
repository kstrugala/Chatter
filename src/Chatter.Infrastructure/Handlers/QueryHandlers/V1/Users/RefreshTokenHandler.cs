using AutoMapper;
using Chatter.Infrastructure.CQRS.Queries;
using Chatter.Infrastructure.Queries.V1.Users;
using Chatter.Infrastructure.Responses.V1.Users;
using Chatter.Infrastructure.Services;
using System.Threading.Tasks;

namespace Chatter.Infrastructure.Handlers.QueryHandlers.V1.Users
{
    public class RefreshTokenHandler : IQueryHandler<RefreshTokenQuery, JsonWebTokenDto>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RefreshTokenHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<JsonWebTokenDto> HandleAsync(RefreshTokenQuery query)
        {
            var token = await _userService.RefreshAccessTokenAsync(query.Token, query.RefreshToken);
            return _mapper.Map<JsonWebTokenDto>(token);
        }
    }
}
