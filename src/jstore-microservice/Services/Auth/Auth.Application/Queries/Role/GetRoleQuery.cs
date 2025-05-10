
namespace Auth.Application.Queries.Role
{
    public class GetRoleQuery : IQuery<IList<RoleResponseDto>>
    {
    }

    public class GetRoleQueryHandler : IQueryHandler<GetRoleQuery, IList<RoleResponseDto>>
    {
        private readonly IIdentityService _identityService;
        public GetRoleQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<IList<RoleResponseDto>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await _identityService.GetRolesAsync();
            return roles.Select(r => new RoleResponseDto() { Id = r.id, RoleName = r.roleName, }).ToList();
        }
    }
}
