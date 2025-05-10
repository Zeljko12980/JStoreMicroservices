
using MediatR;

namespace Auth.Application.Commands.Register
{
    public class RegisterUserCommand : ICommand<(bool isSucceed, string userId)>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }

    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, (bool isSucceed, string userId)>
    {
        private readonly IIdentityService _identityService;
        public RegisterUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<(bool isSucceed, string userId)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.RegisterUserAsync(request.Username, request.Password, request.Email, request.FullName);
        }
    }
}
