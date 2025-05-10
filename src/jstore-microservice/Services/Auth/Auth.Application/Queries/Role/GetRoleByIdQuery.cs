
using Auth.Application.Dtos;
using MediatR;

namespace Auth.Application.Queries.Role
{
    public class GetRoleByIdQuery : IQuery<RoleResponseDto>
    {
        public string RoleId { get; set; }
    }

    public class GetRoleQueryByIdHandler : IQueryHandler<GetRoleByIdQuery, RoleResponseDto>
    {
        private readonly IIdentityService _identityService;

        public GetRoleQueryByIdHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<RoleResponseDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _identityService.GetRoleByIdAsync(request.RoleId);
            return new RoleResponseDto() { Id = role.id, RoleName = role.roleName };
        }
    }
}
