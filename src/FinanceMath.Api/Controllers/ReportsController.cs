using FinanceMath.Application.Report.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceMath.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("user-engagement")]
        public async Task<IActionResult> GetUserEngagementReport()
        {
            var result = await _mediator.Send(new GenerateUserEngagementReportQuery());
            return File(result,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "user_engagement.xlsx");
        }

        [HttpGet("activity-overview")]
        public async Task<IActionResult> GetActivityOverviewReport()
        {
            var result = await _mediator.Send(new GenerateActivityOverviewReportQuery());
            return File(result,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "activity_overview.xlsx");
        }

        [HttpGet("challenges-summary")]
        public async Task<IActionResult> GetChallengesSummaryReport()
        {
            var result = await _mediator.Send(new GenerateChallengesSummaryReportQuery());
            return File(result,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "challenges_summary.xlsx");
        }
    }
}