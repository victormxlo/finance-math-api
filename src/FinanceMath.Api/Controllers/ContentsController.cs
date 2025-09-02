using FinanceMath.Application.Contents.Commands;
using FinanceMath.Application.Contents.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceMath.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ContentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Contents
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllContentsQuery());

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid guid)
        {
            var result = await _mediator.Send(new GetContentByIdQuery { Id = guid });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SaveContent([FromBody] SaveContentCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateContent([FromBody] UpdateContentCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteContent(Guid id)
        {
            var result = await _mediator.Send(new DeleteContentCommand { Id = id });

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return NoContent();
        }

        #endregion

        // categories
        // get all
        // get by id
        // get contents by categories id
        // create categories

        // exercises
    }
}