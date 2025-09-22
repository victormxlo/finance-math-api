namespace FinanceMath.Api.Contracts.Requests
{
    public class UpdateLevelRequest
    {
        public int? NewId { get; set; } = null;
        public required string Name { get; set; }
        public int ThresholdExperience { get; set; }
    }
}