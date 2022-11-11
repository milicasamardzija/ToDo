using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Commands.Attachments
{
    public class DeleteAttachmentCommand : IRequest
    {
        public string Id { get; set; }
    }
    public class DeleteAttachmentCommandHandler : IRequestHandler<DeleteAttachmentCommand>
    {
        private readonly ToDoContext _toDoContext;
        public DeleteAttachmentCommandHandler(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }

        public Task<Unit> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
        {
            Assignment assignment = GetAssignment(request.Id);
            assignment.Attachment = null;

            Attachment attachment = GetAttachment(request.Id);
            _toDoContext.Attachment
                        .Remove(attachment);

            _toDoContext.SaveChanges();
            return Task.FromResult(Unit.Value);
        }

        private Attachment GetAttachment(string id)
        {
            return _toDoContext.Attachment
                               .FirstOrDefault(a => a.Id.ToString() == id);
        }

        private Assignment GetAssignment(string id)
        {
            return  _toDoContext.Assignment
                                .Include(a => a.Attachment)
                                .First(a => a.Attachment.Id.ToString() == id); 
        }
    }
}
