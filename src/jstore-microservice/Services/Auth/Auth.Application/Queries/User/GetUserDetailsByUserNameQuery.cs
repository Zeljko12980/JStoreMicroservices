
namespace Auth.Application.Queries.User
{
    public class GetUserDetailsByUserNameQuery : IQuery<UserDetailsResponseDto>
    {
        public string UserName { get; set; }
    }

    public class GetUserDetailsByUserNameQueryHandler : IQueryHandler<GetUserDetailsByUserNameQuery, UserDetailsResponseDto>
    {
        private readonly IIdentityService _identityService;

        public GetUserDetailsByUserNameQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<UserDetailsResponseDto> Handle(GetUserDetailsByUserNameQuery request, CancellationToken cancellationToken)
        {
            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsByUserNameAsync(request.UserName);
            return new UserDetailsResponseDto() { Id = userId, FullName = fullName, UserName = userName, Email = email, Roles = roles };
        }
    }
}
