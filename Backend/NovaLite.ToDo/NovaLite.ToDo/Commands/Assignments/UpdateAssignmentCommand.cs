using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaLite.ToDo.Dto;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Commands.Assignments
{
    public class UpdateAssignmentCommand : IRequest<AssignmentResponse>
    {
        public int Number { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Reminder { get; set; }
        public string IdAttachment { get; set; } = string.Empty;
    }

    public class UpdateAssignmentCommandHandler : IRequestHandler<UpdateAssignmentCommand, AssignmentResponse>
    {
        private readonly ToDoContext _toDoContext;
        public UpdateAssignmentCommandHandler(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }
        public Task<AssignmentResponse> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
        {
            Assignment assignment = GetAssignment(request.Number);

            if (!CheckIfAttachmentExists(request.IdAttachment))
            {
                Attachment attachment = GetAttachment(request.IdAttachment);
                assignment.Attachment = attachment;
            }

            assignment.Subject = request.Subject;
            assignment.Description = request.Description;
            assignment.Reminder = request.Reminder;

            _toDoContext.SaveChanges();
            return Task.FromResult(new AssignmentResponse { Number = assignment.Number, Subject = assignment.Subject, Description = assignment.Description, Reminder = assignment.Reminder, IsExpired = assignment.IsExpired, Attachment = assignment.Attachment });
        }

        private Attachment GetAttachment(string idAttachment)
        {
            return _toDoContext.Attachment
                               .FirstOrDefault(a => a.Id.ToString() == idAttachment);
        }
        private bool CheckIfAttachmentExists(string idAttachment)
        {
            return idAttachment.Equals("");
        }
        private Assignment GetAssignment(int number)
        {
            return  _toDoContext.Assignee
                                .Include(a => a.Assignments)
                                .SelectMany((a) => a.Assignments)
                                .FirstOrDefault(assignment => number == assignment.Number);
        }
    }
}
