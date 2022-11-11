using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaLite.ToDo.Commands.Steps;
using NovaLite.ToDo.Queries.Steps;

namespace NovaLite.ToDo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StepsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StepsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1, [FromQuery] int assignmentNumber = 0)
        {
            var result = await _mediator.Send(new GetAllStepsQuery { PageNumber = pageNumber, PageSize = pageSize, AssignmentNumber = assignmentNumber });
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddStep(CreateStepCommand newStep)
        {
            var result = _mediator.Send(newStep);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateStep(UpdateStepCommand changedStep)
        {
            var result = _mediator.Send(changedStep);
            return Ok(result);
        }

        [HttpDelete()]
        public IActionResult DeleteStep([FromQuery] int numberStep, [FromQuery] int numberAssignment)
        {
            var result = _mediator.Send(new DeleteStepCommand { NumberAssignment = numberAssignment, NumberStep = numberStep});
            return Ok(result);
        }
    }
}
