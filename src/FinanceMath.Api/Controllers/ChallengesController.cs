using AutoMapper;
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
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllChallengesQuery());

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActive()
        {
            var result = await _mediator.Send(new GetAllChallengesQuery { Active = true });

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
                .Send(new DeleteChallengeByIdCommand { Id = id });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpPost("{id:guid}/complete")]
        public async Task<IActionResult> Complete(Guid id, [FromBody] CompleteChallengeRequest request)
        {
            var command = _mapper.Map<CompleteChallengeCommand>(request);
            command.Id = id;

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }
    }
}