
using MediatR;

namespace Auth.Application.Commands.Role.Update
{
    public class UpdateRoleCommand : ICommand<int>
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
    }

    public class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand, int>
    {
        private readonly IIdentityService _identityService;

        public UpdateRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.UpdateRole(request.Id, request.RoleName);
            return result ? 1 : 0;
        }
    }
}
