namespace FinanceMath.Api.Contracts.Requests
{
    public class UpdateChallengeRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string CriteriaKey { get; set; }
        public int ExperienceReward { get; set; }
        public int VirtualCurrencyReward { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}