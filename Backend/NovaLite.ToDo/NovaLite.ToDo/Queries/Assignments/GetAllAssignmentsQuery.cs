using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaLite.ToDo.Dto;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Queries.Assignments
{
    public class GetAllAssignmentsQuery : IRequest<PagedResponse<Assignment>>  
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllAssignmentQueryHandler : IRequestHandler<GetAllAssignmentsQuery, PagedResponse<Assignment>>
    {
        private readonly ToDoContext _toDoContext;
        public GetAllAssignmentQueryHandler(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }

        public async Task<PagedResponse<Assignment>> Handle(GetAllAssignmentsQuery request, CancellationToken cancellationToken)
        {
            return await _toDoContext.Assignee
                    .Include(a => a.Assignments)
                    .SelectMany(a => a.Assignments)
                    .ToPage(request.PageNumber, request.PageSize);
   
        } 
    }
}
