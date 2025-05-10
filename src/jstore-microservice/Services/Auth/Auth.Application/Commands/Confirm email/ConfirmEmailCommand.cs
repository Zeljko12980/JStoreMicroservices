
namespace Auth.Application.Commands.Confirm_email
{
    public class ConfirmEmailCommand : ICommand<bool>
    {
        public string UserId { get; set; }
        public string Token { get; set; }

        public ConfirmEmailCommand(string userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }

    public class ConfirmEmailCommandHandler : ICommandHandler<ConfirmEmailCommand, bool>
    {
        private readonly IIdentityService _identityService;  // Interfejs za pristup identitetu

        public ConfirmEmailCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            // Pozivamo metodu ConfirmEmailAsync iz IdentityService-a
            var result = await _identityService.ConfirmEmailAsync(request.UserId, request.Token);
            return result;
        }
    }
}
