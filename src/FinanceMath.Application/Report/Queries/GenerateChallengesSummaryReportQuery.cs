using MediatR;

namespace FinanceMath.Application.Report.Queries
{
    public record GenerateChallengesSummaryReportQuery() : IRequest<byte[]>;
}
