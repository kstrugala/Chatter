using Chatter.Infrastructure.Commands.V1.Posts;
using Chatter.Infrastructure.CQRS.Dispatchers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chatter.Api.Controllers.V1
{
    [Authorize]
    [Route("api/v{version:apiVersion}/posts")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class PostsController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public PostsController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] AddPostCommand command)
        {
            command.AuthorsEmail = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            await _dispatcher.SendAsync(command);
            return NoContent();
        }
    }
}
