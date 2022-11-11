using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Commands.Assignments
{
    public class CreateAssignmentCommand : IRequest<Assignment>
    {
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Reminder { get; set; }
        public List<Step> Steps { get; set; } = new List<Step>();
    }

    public class CreateAssignmentCommandHandler : IRequestHandler<CreateAssignmentCommand, Assignment>
    {
        private readonly ToDoContext _toDoContext;
        public CreateAssignmentCommandHandler(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }

        public Task<Assignment> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
        {
            Assignee assignee = GetAssignee();
            Assignment assignment = new Assignment
            {
                Subject = request.Subject,
                Description = request.Description,
                Reminder = request.Reminder,
                Steps = request.Steps
            };
            
            assignee.AddAssignment(assignment);
            _toDoContext.SaveChanges();
            return Task.FromResult(assignment);
        }

        private Assignee GetAssignee()
        {
           return _toDoContext.Assignee
                              .Include(a => a.Assignments)
                              .FirstOrDefault();
        }
    }
}
