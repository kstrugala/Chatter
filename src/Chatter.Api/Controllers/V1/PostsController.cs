using Chatter.Infrastructure.Commands.V1.Posts;
using Chatter.Infrastructure.CQRS.Dispatchers;
using Chatter.Infrastructure.Queries.V1.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPost(Guid id)
        {
            var query = new GetPostQuery { Id = id };
            var post = await _dispatcher.QueryAsync(query);

            if (post is null)
                return NotFound();

            return Ok(post);
        }


        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] AddPostCommand command)
        {
            command.AuthorsEmail = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            await _dispatcher.SendAsync(command);
            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] UpdatePostCommand command)
        {
            command.Id = id;
            await _dispatcher.SendAsync(command);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemovePost(Guid id)
        {
            var command = new RemovePostCommand { Id = id };
            await _dispatcher.SendAsync(command);
            return NoContent();
        }
    }
}
