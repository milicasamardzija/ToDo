using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaLite.ToDo.Queries.Assignees;

namespace NovaLite.ToDo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserResolverController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserResolverController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            var result = _mediator.Send(new GetUserQuery { });
            return Ok(result);
        }
    }
}
