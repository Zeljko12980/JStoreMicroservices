
namespace Auth.Application.Commands.Role.Create
{
    public class RoleCreateCommand : ICommand<int>
    {
        public string RoleName { get; set; }
    }

    public class RoleCreateCommandHandler : ICommandHandler<RoleCreateCommand, int>
    {
        private readonly IIdentityService _identityService;
        public RoleCreateCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(RoleCreateCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.CreateRoleAsync(request.RoleName);
            return result ? 1 : 0;
        }
    }
}
