
using Microsoft.AspNetCore.Authorization;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class RoleController : ControllerBase
    {
        public readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> CreateRoleAsync(RoleCreateCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet("GetAll")]
        [ProducesDefaultResponseType(typeof(List<RoleResponseDto>))]
        public async Task<IActionResult> GetRoleAsync()
            => Ok(await _mediator.Send(new GetRoleQuery()));

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(RoleResponseDto))]
        public async Task<IActionResult> GetRoleByIdAsync(string id)
            => Ok(await _mediator.Send(new GetRoleByIdQuery() { RoleId = id }));


        [HttpDelete("Delete/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteRoleAsync(string id)
            => Ok(await _mediator.Send(new DeleteRoleCommand() { RoleId = id }));

        [HttpPut("Edit/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> EditRoleAsync(string id, [FromBody] UpdateRoleCommand command)
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
