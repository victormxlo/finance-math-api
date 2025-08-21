using FinanceMath.Application.Users.Commands.RegisterUser;
using FinanceMath.Application.Users.Queries.GetUserById;
using FinanceMath.Application.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceMath.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var result = await _mediator.Send(new RegisterUserCommand { Request = request });

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Value.Id },
                result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery { Id = id });

            if (!result.Success)
                return NotFound(new { error = result.Error });

            return Ok(result.Value);
        }

        // POST register
        // POST login
        // GET getById
        // PATCH updateUsername
        // PATCH updatePassword
    }
}