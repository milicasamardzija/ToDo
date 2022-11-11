using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Queries.Assignments
{
    public class GetAssignmentByIdQuery : IRequest<Assignment> 
    {
        public int Id { get; set; }
    }

    public class GetAssignmentByIdQueryHandler : IRequestHandler<GetAssignmentByIdQuery, Assignment>
    {
        private readonly ToDoContext _toDoContext;
        public GetAssignmentByIdQueryHandler(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }
        public Task<Assignment> Handle(GetAssignmentByIdQuery request, CancellationToken cancellationToken)
        {
            Assignment assignment = _toDoContext.Assignee
                                                 .Include(a => a.Assignments)
                                                 .SelectMany((a) => a.Assignments)
                                                 .Include(a => a.Attachment)
                                                 .FirstOrDefault(assignment => assignment.Number == request.Id);
            return Task.FromResult(assignment);
        }
    }
}
