namespace FinanceMath.Application.Gamification.Challenges.Dtos
{
    public class CompleteChallengeResponseDto
    {
        public Guid ChallengeId { get; set; }
        public int XpAwarded { get; set; }
        public int VirtualCurrencyAwarded { get; set; }
        public int TotalXp { get; set; }
        public int LevelId { get; set; }
        public required string LevelName { get; set; }
    }
}
