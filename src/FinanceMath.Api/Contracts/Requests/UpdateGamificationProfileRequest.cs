namespace FinanceMath.Api.Contracts.Requests
{
    public class UpdateGamificationProfileRequest
    {
        public int? ExperiencePoints { get; set; }
        public int? VirtualCurrency { get; set; }
        public int? LevelId { get; set; }
        public DateTime? ActivityDate { get; set; }
    }
}