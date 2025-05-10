
namespace Auth.Application.Commands.Role.Delete
{
    public class DeleteRoleCommand : ICommand<int>
    {
        public string RoleId { get; set; }
    }

    public class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand, int>
    {
        private readonly IIdentityService _identityService;
        public DeleteRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.DeleteRoleAsync(request.RoleId);
            return result ? 1 : 0;
        }
    }
}
