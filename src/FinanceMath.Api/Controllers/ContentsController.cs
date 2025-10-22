using AutoMapper;
using FinanceMath.Api.Contracts.Requests;
using FinanceMath.Application.Content.Contents.Commands;
using FinanceMath.Application.Content.Contents.Queries;
using FinanceMath.Application.Content.ContentSections.Commands;
using FinanceMath.Application.Content.ContentSections.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceMath.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ContentsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        #region Contents
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllContentsQuery());

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetContentByIdQuery { Id = id });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContentCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContentRequest request)
        {
            var command = _mapper.Map<UpdateContentCommand>(request);
            command.Id = id;

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteContentCommand { Id = id });

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return NoContent();
        }

        [HttpPost("{id:guid}/complete")]
        public async Task<IActionResult> Complete(
            Guid id, [FromBody] CompleteContentRequest request)
        {
            var result = await _mediator.Send(
                new CompleteContentCommand
                { ContentId = id, UserId = request.UserId });

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpGet("{userId:guid}/progress")]
        public async Task<IActionResult> GetProgress(Guid userId)
        {
            var result = await _mediator.Send(new GetUserContentProgressQuery { UserId = userId });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }
        #endregion

        #region Content Sections
        [HttpGet("{contentId:guid}/sections")]
        public async Task<IActionResult> GetAllContentSectionsByContentId(Guid contentId)
        {
            var result = await _mediator
                .Send(new GetAllContentSectionsByContentIdQuery
                { ContentId = contentId });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpGet("{contentId:guid}/sections/{sectionId:guid}")]
        public async Task<IActionResult> GetContentSectionById(Guid contentId, Guid sectionId)
        {
            var result = await _mediator
                .Send(new GetContentSectionByIdQuery
                { ContentId = contentId, ContentSectionId = sectionId });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpPost("{contentId:guid}/sections")]
        public async Task<IActionResult> CreateContentSection(Guid contentId, [FromBody] CreateContentSectionRequest request)
        {
            var command = await _mediator.Send(new CreateContentSectionCommand
            {
                ContentId = contentId,
                Title = request.Title,
                Body = request.Body,
                Order = request.Order
            });

            if (!command.Success)
                return BadRequest(new { error = command.Error });

            return Ok(command.Value);
        }

        [HttpPut("{contentId:guid}/sections")]
        public async Task<IActionResult> UpdateContentSection(Guid contentId, [FromBody] UpdateContentSectionRequest request)
        {
            var command = await _mediator.Send(new UpdateContentSectionCommand
            {
                ContentId = contentId,
                ContentSectionId = request.ContentSectionId,
                Title = request.Title,
                Body = request.Body,
                Order = request.Order
            });

            if (!command.Success)
                return BadRequest(new { error = command.Error });

            return Ok(command.Value);
        }

        [HttpDelete("{contentId:guid}/sections/{sectionId:guid}")]
        public async Task<IActionResult> Delete(Guid contentId, Guid sectionId)
        {
            var result = await _mediator
                .Send(new DeleteContentSectionCommand
                { ContentId = contentId, ContentSectionId = sectionId });

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return NoContent();
        }
        #endregion

        [HttpPost("{contentId:guid}/link-exercise")]
        public async Task<IActionResult> LinkExercise(Guid contentId, [FromBody] LinkContentExerciseRequest request)
        {
            var result = await _mediator.Send(
                new LinkContentExerciseCommand
                { ContentId = contentId, ExerciseId = request.ExerciseId });

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }
    }
}