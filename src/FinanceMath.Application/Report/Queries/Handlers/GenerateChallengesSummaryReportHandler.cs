using FinanceMath.Application.Interfaces;
using MediatR;

namespace FinanceMath.Application.Report.Queries.Handlers
{
    public class GenerateChallengesSummaryReportHandler : IRequestHandler<GenerateChallengesSummaryReportQuery, byte[]>
    {
        private readonly IReportService _reportService;

        public GenerateChallengesSummaryReportHandler(IReportService IReportService)
        {
            _reportService = IReportService;
        }

        public async Task<byte[]> Handle(GenerateChallengesSummaryReportQuery request, CancellationToken cancellationToken)
            => await _reportService.GenerateChallengesSummaryReportAsync();
    }
}
