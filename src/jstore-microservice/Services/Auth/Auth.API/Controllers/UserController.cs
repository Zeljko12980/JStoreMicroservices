
using Microsoft.AspNetCore.Authorization;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize("SuperAdmin")]
        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> CreateUser(CreateUserCommand userCommand)
            => Ok(await _mediator.Send(userCommand));

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> GetAllUserAsync()
            => Ok(await _mediator.Send(new Application.Queries.User.GetUserQuery()));


        [HttpDelete("Delete/{userId}")]
        [Authorize("SuperAdmin")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _mediator.Send(new DeleteUserCommand() { Id = userId });
            return Ok(result);
        }

        [HttpGet("GetUserDetails/{userId}")]
        [Authorize]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDto))]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            var result = await _mediator.Send(new GetUserDetailsQuery() { UserId = userId });
            return Ok(result);
        }

        [HttpGet("GetUserDetailsByUserName/{userName}")]
        [Authorize(Roles ="Admin,SuperAdmin")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDto))]
        public async Task<IActionResult> GetUserDetailsByUserName(string userName)
        {
            var result = await _mediator.Send(new GetUserDetailsByUserNameQuery() { UserName = userName });
            return Ok(result);
        }

        [HttpPost("AssignRoles")]
        [ProducesDefaultResponseType(typeof(int))]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> AssignRoles(AssignUsersRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("EditUserRoles")]
        [ProducesDefaultResponseType(typeof(int))]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> EditUserRoles(UpdateUserRolesCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetAllUserDetails")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDto))]
        public async Task<IActionResult> GetAllUserDetails()
        {
            var result = await _mediator.Send(new GetAllUsersDetailsQuery());
            return Ok(result);
        }

        [HttpPut("EditUserProfile/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUserProfile(string id, [FromBody] EditUserProfileCommand command)
        {
            if (id == command.Id)
            {
                var result = await _mediator.Send(command);
                return Ok(result);

            }
            else
            {
                return BadRequest();
            }
        }

    }
}
