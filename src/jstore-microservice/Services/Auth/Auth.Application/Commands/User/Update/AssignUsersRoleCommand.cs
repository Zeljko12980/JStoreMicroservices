
namespace Auth.Application.Commands.User.Update
{
    public class AssignUsersRoleCommand : ICommand<int>
    {
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class AssignUsersRoleCommandHandler : ICommandHandler<AssignUsersRoleCommand, int>
    {
        private readonly IIdentityService _identityService;

        public AssignUsersRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(AssignUsersRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.AssignUserToRole(request.UserName, request.Roles);
            return result ? 1 : 0;
        }
    }
}
