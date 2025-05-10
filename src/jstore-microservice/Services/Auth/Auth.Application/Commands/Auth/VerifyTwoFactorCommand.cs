

using FluentValidation.Validators;

namespace Auth.Application.Commands.Auth
{
    public record VerifyTwoFactorCommand(string Email, string Code) : ICommand<AuthResponseDto>;

    public class VerifyTwoFactorCommandHandler : ICommandHandler<VerifyTwoFactorCommand, AuthResponseDto>
    {
        private readonly IIdentityService _identityService;

        public VerifyTwoFactorCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<AuthResponseDto> Handle(VerifyTwoFactorCommand request, CancellationToken cancellationToken)
        {
            var (userId,userName,token)=await _identityService.VerifyTwoFactorCodeAsync(request.Email, request.Code);

            return new AuthResponseDto
            {
                UserId = userId,
                Name = userName,
                Token = token
            };
        }
    }
}
