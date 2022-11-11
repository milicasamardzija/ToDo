using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaLite.ToDo.Commands.Attachments;
using NovaLite.ToDo.Queries.Attachments;

namespace NovaLite.ToDo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AttachmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttachmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public IActionResult GetTokenForDownload(string id)
        {
            var result = _mediator.Send(new GetTokenForAttachmentDownloadQuery { Id = id });
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddAttachment(CreateAttachmentCommand newAttachment)
        {
            var result = _mediator.Send(newAttachment);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAttachment(string id)
        {
            var result = _mediator.Send(new DeleteAttachmentCommand { Id = id});
            return Ok(result);
        }
    }
}
