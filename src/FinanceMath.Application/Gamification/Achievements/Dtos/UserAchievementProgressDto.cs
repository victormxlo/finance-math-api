namespace FinanceMath.Application.Gamification.Achievements.Dtos
{
    public class UserAchievementProgressDto
    {
        public Guid ProfileId { get; set; }
        public Guid UserId { get; set; }
        public Guid AchievementId { get; set; }
        public required string AchievementName { get; set; }
        public required string CriteriaKey { get; set; }
        public DateTime UnlockedAt { get; set; }
    }
}
