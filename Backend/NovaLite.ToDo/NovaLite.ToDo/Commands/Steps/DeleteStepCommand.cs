using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Commands.Steps
{
    public class DeleteStepCommand : IRequest
    {
        public int NumberAssignment { get; set; }
        public int NumberStep { get; set; }
    }
    public class DeleteStepCommandHandler : IRequestHandler<DeleteStepCommand>
    {
        private readonly ToDoContext _toDoContext;
        public DeleteStepCommandHandler(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }

        public Task<Unit> Handle(DeleteStepCommand request, CancellationToken cancellationToken)
        {
            Step step = GetStep(request.NumberAssignment, request.NumberStep);

            _toDoContext.Step
                        .Remove(step);
            _toDoContext.SaveChanges();

            return Task.FromResult(Unit.Value);
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
