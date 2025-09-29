using AutoMapper;
using FinanceMath.Api.Contracts.Requests;
using FinanceMath.Application.Gamification.Achievements.Commands;
using FinanceMath.Application.Gamification.Achievements.Queries;
using FinanceMath.Application.Gamification.Leaderboards.Queries;
using FinanceMath.Application.Gamification.Profiles.Commands;
using FinanceMath.Application.Gamification.Profiles.Queries;
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

        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard([FromQuery] int? top)
        {
            var result = await _mediator.Send(new GetLeaderboardQuery { Top = top });

            if (!result.Success)
                return NotFound();

            return Ok(result.Value);
        }

        #region Experience Points
        [HttpPost("{userId:guid}/experience-points/grant")]
        public async Task<IActionResult> GrantUserExperiencePoints(
            Guid userId, [FromBody] GrantUserExperiencePointsRequest request)
        {
            var result = await _mediator.Send(
                new GrantUserExperiencePointsCommand
                { UserId = userId, ExperiencePointsAmount = request.Amount });

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }
        #endregion

        #region Virtual Currency
        [HttpPost("{userId:guid}/currencies/grant")]
        public async Task<IActionResult> GrantUserVirtualCurrency(
            Guid userId, [FromBody] GrantUserVirtualCurrencyRequest request)
        {
            var result = await _mediator.Send(
                new GrantUserVirtualCurrencyCommand
                { UserId = userId, VirtualCurrencyAmount = request.Amount });

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

        [HttpPost("{userId:guid}/achievements/unlock")]
        public async Task<IActionResult> UnlockAchievement(
            Guid userId, [FromBody] UnlockAchievementRequest request)
        {
            var result = await _mediator.Send(new UnlockUserAchievementCommand
            { UserId = userId, AchievementId = request.AchievementId, CriteriaKey = request.CriteriaKey });

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result.Value);
        }
        #endregion
    }
}