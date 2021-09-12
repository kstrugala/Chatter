using Chatter.Infrastructure.Commands.V1.Users;
using Chatter.Infrastructure.CQRS.Dispatchers;
using Chatter.Infrastructure.Queries.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatter.Api.Controllers
{
    [ApiController]
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
            var result  = await _dispatcher.QueryAsync(query);
            return Ok(result);
        }
    }
}
