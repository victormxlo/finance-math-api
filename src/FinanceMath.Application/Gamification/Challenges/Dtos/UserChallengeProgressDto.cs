namespace FinanceMath.Application.Gamification.Challenges.Dtos
{
    public class UserChallengeProgressDto
    {
        public Guid ChallengeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CurrentProgress { get; set; }
        public int TargetProgress { get; set; }
        public bool IsCompleted { get; set; }
    }
}
