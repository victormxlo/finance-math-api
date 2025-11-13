using FinanceMath.Application.Interfaces;
using MediatR;

namespace FinanceMath.Application.Report.Queries.Handlers
{
    public class GenerateUserEngagementReportHandler : IRequestHandler<GenerateUserEngagementReportQuery, byte[]>
    {
        private readonly IReportService _reportService;

        public GenerateUserEngagementReportHandler(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<byte[]> Handle(GenerateUserEngagementReportQuery request, CancellationToken cancellationToken)
            => await _reportService.GenerateUserEngagementReportAsync();
    }
}
