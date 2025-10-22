using AutoMapper;
using FinanceMath.Api.Contracts.Requests;
using FinanceMath.Application.Gamification.Challenges.Commands;
using FinanceMath.Application.Gamification.Challenges.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceMath.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChallengesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ChallengesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? active)
        {
            var result = await _mediator.Send(new GetAllChallengesQuery { Active = active ?? false });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator
                .Send(new GetChallengeByIdQuery { Id = id });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateChallengeCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateChallengeRequest request)
        {
            var command = _mapper.Map<UpdateChallengeCommand>(request);
            command.Id = id;

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator
                .Send(new DeleteChallengeCommand { Id = id });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpPost("{id:guid}/complete")]
        public async Task<IActionResult> Complete(Guid id, [FromBody] CompleteChallengeRequest request)
        {
            var command = _mapper.Map<CompleteChallengeCommand>(request);
            command.ChallengeId = id;

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpGet("{userId:guid}/progress")]
        public async Task<IActionResult> GetProgress(Guid userId)
        {
            var result = await _mediator.Send(new GetUserChallengeProgressQuery { UserId = userId });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }
    }
}