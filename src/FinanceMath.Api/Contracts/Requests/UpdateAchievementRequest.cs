namespace FinanceMath.Api.Contracts.Requests
{
    public class UpdateAchievementRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string CriteriaKey { get; set; }
        public int ExperienceReward { get; set; }
        public int VirtualCurrencyReward { get; set; }
    }
}