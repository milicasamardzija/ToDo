using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaLite.ToDo.Model;
using NovaLite.ToDo.Queries.Assignments;

namespace NovaLite.ToDo.Commands.Assignments
{
    public class DeleteAssignmentCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteAssignmentCommandHandler : IRequestHandler<DeleteAssignmentCommand>
    {
        private readonly ToDoContext _toDoContext;
        public DeleteAssignmentCommandHandler(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }

        public Task<Unit> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
        {
            Assignment assignment = GetAssignmentByIdQuery(request);

            _toDoContext.Step.RemoveRange(assignment.Steps);
            _toDoContext.Assignment.Remove(assignment);
            _toDoContext.SaveChanges();

            return Unit.Task;
        }

        private Assignment GetAssignmentByIdQuery(DeleteAssignmentCommand request)
        {
            return  _toDoContext.Assignee
                                .Include(a => a.Assignments)
                                .SelectMany((a) => a.Assignments)
                                .Include(a => a.Steps)
                                .FirstOrDefault(assignment => assignment.Number == request.Id);
        }
    }

}
