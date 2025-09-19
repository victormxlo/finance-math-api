using AutoMapper;
using FinanceMath.Api.Contracts.Requests;
using FinanceMath.Application.Gamification.Profiles.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceMath.Api.Controllers
{
    [ApiController]
    [Route("api/gamification/profiles")]
    [Authorize]
    public class GamificationProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GamificationProfilesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var result = await _mediator
                .Send(new GetGamificationProfileByUserIdQuery { UserId = userId });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateGamificationProfileCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> UpdateByUserId(
            Guid userId, [FromBody] UpdateGamificationProfileRequest request)
        {
            var command = _mapper.Map<UpdateGamificationProfileCommand>(request);
            command.UserId = userId;

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }
        #region Experience Points
        [HttpPost("{userId:guid}/xp")]
        public async Task<IActionResult> GrantExperiencePoints(
            Guid userId, [FromBody] GrantExperiencePointsRequest request)
        {
            var command = _mapper.Map<GrantExperiencePointsCommand>(request);
            command.UserId = userId;

            var result = await _mediator.Send(request);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }
        #endregion

        #region Virtual Currency
        [HttpPost("{userId:guid}/currency")]
        public async Task<IActionResult> GrantVirtualCurrency(
            Guid userId, [FromBody] GrantVirtualCurrencyRequest request)
        {
            var command = _mapper.Map<GrantVirtualCurrencyCommand>(request);
            command.UserId = userId;

            var result = await _mediator.Send(request);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }
        #endregion

        #region Achievements
        [HttpGet("{userId:guid}/achievements")]
        public async Task<IActionResult> GetAchievementsByUserId(Guid userId)
        {
            var result = await _mediator
                .Send(new GetAchievementsByUserIdQuery { UserId = userId });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpPost("{userId:guid}/achievements/{achievementId:guid}")]
        public async Task<IActionResult> UnlockAchievement(
            Guid userId, Guid achievementId, [FromBody] UnlockAchievementRequest request)
        {
            var command = _mapper.Map<UnlockAchievementCommand>(request);
            command.UserId = userId;
            command.AchievementId = achievementId;

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }
        #endregion
    }
}