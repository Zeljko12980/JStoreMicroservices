
using Auth.Application.Dtos;
using MediatR;

namespace Auth.Application.Queries.User
{
    public class GetUserQuery : IQuery<List<UserResponseDto>>
    {
    }

    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, List<UserResponseDto>>
    {
        private readonly IIdentityService _identityService;

        public GetUserQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<List<UserResponseDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _identityService.GetAllUsersAsync(cancellationToken);
            return users.Select(x => new UserResponseDto()
            {
                Id = x.id,
                FullName = x.fullName,
                UserName = x.userName,
                Email = x.email
            }).ToList();
        }
    }
}
