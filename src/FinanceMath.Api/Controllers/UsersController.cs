using FinanceMath.Api.Contracts.Requests;
using FinanceMath.Application.Users.Commands;
using FinanceMath.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceMath.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery { Id = id });

            if (!result.Success)
                return NotFound(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpPatch("{id:guid}/change-username")]
        public async Task<IActionResult> ChangeUsername(Guid id, [FromBody] ChangeUsernameRequest request)
        {
            ChangeUsernameCommand command = new ChangeUsernameCommand
            {
                Id = id,
                NewUsername = request.NewUsername
            };

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpPatch("{id:guid}/change-password")]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangeUserPasswordRequest request)
        {
            ChangeUserPasswordCommand command = new ChangeUserPasswordCommand
            {
                Id = id,
                CurrentPassword = request.CurrentPassword,
                NewPassword = request.NewPassword
            };

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }
    }
}