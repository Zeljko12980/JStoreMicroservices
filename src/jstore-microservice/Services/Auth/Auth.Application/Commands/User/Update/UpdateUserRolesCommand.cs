
using MediatR;

namespace Auth.Application.Commands.User.Update
{
    public class UpdateUserRolesCommand : ICommand<int>
    {
        public string userName { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class UpdateUserRolesCommandHandler : ICommandHandler<UpdateUserRolesCommand, int>
    {
        private readonly IIdentityService _identityService;
        public UpdateUserRolesCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.UpdateUsersRole(request.userName, request.Roles);
            return result ? 1 : 0;
        }
    }
}
