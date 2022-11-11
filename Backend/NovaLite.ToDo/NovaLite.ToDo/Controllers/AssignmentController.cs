using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NovaLite.ToDo.Commands.Assignments;
using NovaLite.ToDo.Queries.Assignments;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NovaLite.ToDo.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AssignmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssignmentController (IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1)
        {
            var result = await _mediator.Send(new GetAllAssignmentsQuery { PageNumber = pageNumber, PageSize = pageSize });
            return Ok(result);
        }

        [HttpGet("{number}")]
        public IActionResult GetAssignment(int number)
        {
            var result = _mediator.Send(new GetAssignmentByIdQuery { Id = number });
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddAssignment(CreateAssignmentCommand assignmentNew)
        {
            var result = _mediator.Send(assignmentNew);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateAssignment(UpdateAssignmentCommand changedAssignment)
        {
            var result = _mediator.Send(changedAssignment);
            return Ok(result);
        }

        [HttpDelete("{number}")]
        public IActionResult DeleteAssignment(int number)
        {
            var result = _mediator.Send(new DeleteAssignmentCommand { Id = number });
            return Ok(result);
        }
    }
}
