using Chatter.Infrastructure.Commands.V1.Users;
using Chatter.Infrastructure.CQRS.Dispatchers;
using Chatter.Infrastructure.Queries.V1.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chatter.Api.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class UsersController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public UsersController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
        {
            await _dispatcher.SendAsync(command);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInQuery query)
        {
            var result = await _dispatcher.QueryAsync(query);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token/refresh")]
        public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshTokenQuery query)
            => Ok(await _dispatcher.QueryAsync(query));

        [HttpPost]
        [Route("token/revoke")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenCommand command)
        {
            await _dispatcher.SendAsync(command);
            return NoContent();
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            command.Email = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            await _dispatcher.SendAsync(command);
            return NoContent();
        }
    }
}
