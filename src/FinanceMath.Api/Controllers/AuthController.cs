using FinanceMath.Application.Users.Commands.LoginUser;
using FinanceMath.Application.Users.Commands.RegisterUser;
using FinanceMath.Application.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceMath.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var result = await _mediator.Send(new RegisterUserCommand { Request = request });

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return CreatedAtAction(
                nameof(UsersController.GetById),
                new { id = result.Value!.Id },
                result.Value);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var result = await _mediator.Send(new LoginUserCommand { Request = request });

            if (!result.Success)
                return Unauthorized(new { error = result.Error });

            return Ok(result.Value);
        }
    }
}