namespace FinanceMath.Application.Gamification.Challenges.Dtos
{
    public class UserChallengeProgressDto
    {
        public Guid ProfileId { get; set; }
        public Guid UserId { get; set; }
        public Guid ChallengeId { get; set; }
        public string ChallengeName { get; set; } = string.Empty;
        public required string CriteriaKey { get; set; }
        public int CurrentProgress { get; set; }
        public int TargetProgress { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
