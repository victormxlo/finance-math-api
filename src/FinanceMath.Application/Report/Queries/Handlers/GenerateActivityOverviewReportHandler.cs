using FinanceMath.Application.Interfaces;
using MediatR;

namespace FinanceMath.Application.Report.Queries.Handlers
{
    public class GenerateActivityOverviewReportHandler : IRequestHandler<GenerateActivityOverviewReportQuery, byte[]>
    {
        private readonly IReportService _reportService;

        public GenerateActivityOverviewReportHandler(IReportService IReportService)
        {
            _reportService = IReportService;
        }

        public async Task<byte[]> Handle(GenerateActivityOverviewReportQuery request, CancellationToken cancellationToken)
            => await _reportService.GenerateActivityOverviewReportAsync();
    }
}
