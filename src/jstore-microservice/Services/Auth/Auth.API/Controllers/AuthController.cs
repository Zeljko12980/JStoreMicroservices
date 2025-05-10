
using Auth.Application.Commands.Confirm_email;
using Auth.Application.Commands.Register;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.isSucceed)
            {
                return Ok(new { UserId = result.userId });
            }

            return BadRequest("Registration failed.");
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _mediator.Send(new ConfirmEmailCommand(userId, token));

            if (result)
            {
                return Ok("Email confirmed successfully.");
            }

            return BadRequest("Error confirming email.");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthCommand command)
        {
            await _mediator.Send(command);
            return Ok("2FA code sent.");
        }

        [HttpPost("login/verify-2fa")]
        public async Task<IActionResult> VerifyTwoFactorCode([FromBody] VerifyTwoFactorCommand command, CancellationToken cancellationToken)
        {
            // Call the mediator to handle the command
            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return Unauthorized(new { message = "Invalid two-factor authentication code" });
            }

            // Return the successful response (you can return the token, or any necessary data)
            return Ok(new
            {
                userId = result.UserId,
                userName = result.Name,
                token = result.Token
            });
        }
    }
}
