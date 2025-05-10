
namespace Auth.Application.Commands.Auth
{
    public class AuthCommand : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthCommandHandler : ICommandHandler<AuthCommand>
    {

        private readonly IIdentityService _identityService;
        public AuthCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
                  
        }

        public async Task<Unit> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            await _identityService.LoginAsync(request.Email, request.Password);
            return Unit.Value;
        }
    }
}
