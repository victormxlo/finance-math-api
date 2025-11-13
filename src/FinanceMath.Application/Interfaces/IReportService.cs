namespace FinanceMath.Application.Interfaces
{
    public interface IReportService
    {
        Task<byte[]> GenerateUserEngagementReportAsync(DateTime? lastActivitySince = null, CancellationToken cancellation = default);

        Task<byte[]> GenerateActivityOverviewReportAsync(CancellationToken cancellation = default);

        Task<byte[]> GenerateChallengesSummaryReportAsync(CancellationToken cancellation = default);
    }
}
