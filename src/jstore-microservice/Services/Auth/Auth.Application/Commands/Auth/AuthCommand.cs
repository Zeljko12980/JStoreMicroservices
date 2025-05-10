
namespace Auth.Application.Commands.Auth
{
    public class AuthCommand : ICommand<AuthResponseDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthCommandHandler : ICommandHandler<AuthCommand, AuthResponseDto>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IIdentityService _identityService;
        public AuthCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator;
       
        }

        public async Task<AuthResponseDto> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.SigninUserAsync(request.UserName, request.Password);

            if (!result)
            {
                throw new BadRequestException("Invalid username or password");

            }
            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsAsync(
                await _identityService.GetUserIdAsync(request.UserName)
                );

            string token = _tokenGenerator.GenerateJWTToken((userId, userName, roles));

            return new AuthResponseDto()
            {
                UserId = userId,
                Name = userName,
                Token = token
            };
        }
    }
}
