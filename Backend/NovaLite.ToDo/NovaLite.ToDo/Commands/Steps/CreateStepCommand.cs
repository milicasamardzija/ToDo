using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Commands.Steps
{
    public class CreateStepCommand : IRequest<Step>
    {
        public int AssignmentNumber { get; set; }
        public string Subject { get; set; } = string.Empty;
        public bool Completed { get; set; }
    }
    public class CreateStepCommandHandler : IRequestHandler<CreateStepCommand, Step>
    {
        private readonly ToDoContext _toDoContext;
        public CreateStepCommandHandler(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }
        public Task<Step> Handle(CreateStepCommand request, CancellationToken cancellationToken)
        {
            Assignment assignment = GetAssignment(request.AssignmentNumber);
            Step step = new() { Subject = request.Subject, Completed = request.Completed};

            assignment.Steps.Add(step);
            _toDoContext.SaveChanges();
            return Task.FromResult(step);
        }

        private Assignment GetAssignment(int assignmentNumber)
        {
            return  _toDoContext.Assignee
                                .Include(a => a.Assignments)
                                .SelectMany(a => a.Assignments)
                                .Include(a => a.Steps)
                                .FirstOrDefault(assignment => assignmentNumber == assignment.Number);
        }
    }
}
