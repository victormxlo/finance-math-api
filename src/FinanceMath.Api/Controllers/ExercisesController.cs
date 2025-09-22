using AutoMapper;
using FinanceMath.Api.Contracts.Requests;
using FinanceMath.Api.Contracts.Responses;
using FinanceMath.Application.Content.Exercises.Commands;
using FinanceMath.Application.Exercises.Commands;
using FinanceMath.Application.Exercises.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceMath.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExercisesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ExercisesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        #region Exercises
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllExercisesQuery());

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetExerciseByIdQuery { Id = id });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExerciseCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateExerciseCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteExerciseCommand { Id = id });

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return NoContent();
        }

        [HttpPost("{id:guid}")]
        public async Task<IActionResult> ValidateAnswer(Guid id, ValidateExerciseAnswerRequest request)
        {
            var command = await _mediator
                .Send(new ValidateExerciseAnswerCommand
                { UserId = request.UserId, ExerciseId = id, ExerciseOptionId = request.ExerciseOptionId });

            if (!command.Success)
                return BadRequest(new { error = command.Error });

            var response = _mapper.Map<ValidateExerciseAnswerResponse>(command.Value);

            return Ok(response);
        }
        #endregion

        #region Hints
        [HttpGet("{id:guid}/hints")]
        public async Task<IActionResult> GetHintsByExerciseId(Guid id)
        {
            var result = await _mediator.Send(new GetHintsByExerciseIdQuery { Id = id });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }
        #endregion

        #region Explanation
        [HttpGet("{id:guid}/explanation")]
        public async Task<IActionResult> GetExplanationByExerciseId(Guid id)
        {
            var result = await _mediator.Send(new GetExplanationByExerciseIdQuery { Id = id });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }
        #endregion
    }
}