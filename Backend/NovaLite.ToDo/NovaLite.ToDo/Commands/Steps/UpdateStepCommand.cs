using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Commands.Steps
{
    public class UpdateStepCommand : IRequest<Step>
    {
        public int AssignmentNumber { get; set; }
        public int Number { get; set; }
        public string Subject { get; set; } = string.Empty;
        public bool Completed { get; set; }
    }
    public class UpdateStepCommandHandler : IRequestHandler<UpdateStepCommand, Step>
    {
        private readonly ToDoContext _toDoContext;
        public UpdateStepCommandHandler(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }
        public Task<Step> Handle(UpdateStepCommand request, CancellationToken cancellationToken)
        {
            Step step = GetStep(request.AssignmentNumber, request.Number);

            step.Subject = request.Subject;
            step.Completed = request.Completed;
            _toDoContext.SaveChanges();

            return Task.FromResult(step);
        }

        private Step GetStep(int assignmentNumber, int number)
        {
            return _toDoContext.Assignee
                                .Include(a => a.Assignments)
                                .SelectMany(a => a.Assignments)
                                .Include(a => a.Steps)
                                .FirstOrDefault(assignment => assignmentNumber == assignment.Number)
                                .Steps
                                .FirstOrDefault(step => step.Number == number);
        }
    }
}
