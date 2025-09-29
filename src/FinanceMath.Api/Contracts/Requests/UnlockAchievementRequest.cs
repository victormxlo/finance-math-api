namespace FinanceMath.Api.Contracts.Requests
{
    public class UnlockAchievementRequest
    {
        public Guid AchievementId { get; set; }
        public required string CriteriaKey { get; set; }
    }
}