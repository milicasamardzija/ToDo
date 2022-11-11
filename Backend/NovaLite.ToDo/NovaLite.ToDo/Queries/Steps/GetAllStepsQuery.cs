using MediatR;
using NovaLite.ToDo.Dto;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Queries.Steps
{
    public class GetAllStepsQuery : IRequest<PagedResponse<Step>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int AssignmentNumber { get; set; }
    }
    public class GetAllStepsQueryHandler : IRequestHandler<GetAllStepsQuery, PagedResponse<Step>>
    {
        private readonly ToDoContext _toDoContext;
        public GetAllStepsQueryHandler(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }
        public async Task<PagedResponse<Step>> Handle(GetAllStepsQuery request, CancellationToken cancellationToken)
        {
            return await _toDoContext.Assignee
                            .SelectMany(a => a.Assignments)
                            .Where(a => a.Number == request.AssignmentNumber)
                            .SelectMany(a => a.Steps)
                            .ToPage(request.PageNumber, request.PageSize);
        }
    }
}
