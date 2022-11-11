using MediatR;
using NovaLite.ToDo.Dto;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Queries.Assignees
{
    public class GetUserQuery : IRequest<UserResponse> { }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponse> 
    {
        private readonly ToDoContext _toDoContext;
        private readonly IHttpContextAccessor _accessor;
        public GetUserQueryHandler(ToDoContext toDoContext, IHttpContextAccessor accessor)
        {
            _toDoContext = toDoContext;
            _accessor = accessor;
        }

        public Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            string email = GetAssigneeEmailFromClaims();
            Assignee assignee = _toDoContext.Assignee
                                            .FirstOrDefault(a => a.Email.Equals(email));
            if (assignee == null)
            {
                Assignee newAssignee = AddNewAssignee(email);
                return Task.FromResult(new UserResponse { Email = newAssignee.Email });
            }

            return Task.FromResult(new UserResponse { Email = assignee.Email , Name = GetAssigneeNameFromClaims()});
        }

        private Assignee AddNewAssignee(string email)
        {
            Assignee newAssignee = new Assignee { Email = email };
            _toDoContext.Set<Assignee>().Add(newAssignee);
            _toDoContext.SaveChanges();
            return newAssignee;
        }

        private string GetAssigneeEmailFromClaims()
        {
            var email = _accessor.HttpContext.User.Claims.First(c => c.Type == "preferred_username");
            return email.Value;
        }

        private string GetAssigneeNameFromClaims()
        {
            var name = _accessor.HttpContext.User.Claims.First(c => c.Type == "name");
            return name.Value;
        }
    }
}
