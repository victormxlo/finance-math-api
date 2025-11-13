using MediatR;

namespace FinanceMath.Application.Report.Queries
{
    public record GenerateUserEngagementReportQuery() : IRequest<byte[]>;
}
