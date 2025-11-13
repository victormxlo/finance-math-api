using MediatR;

namespace FinanceMath.Application.Report.Queries
{
    public record GenerateActivityOverviewReportQuery() : IRequest<byte[]>;
}
