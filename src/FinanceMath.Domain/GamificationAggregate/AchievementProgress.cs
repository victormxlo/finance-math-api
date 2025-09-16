namespace FinanceMath.Domain.GamificationAggregate
{
    public class AchievementProgress
    {
        public virtual Guid AchievementId { get; protected set; }
        public virtual DateTime UnlockedAt { get; protected set; }

        protected AchievementProgress() { }

        public AchievementProgress(Guid achievementId, DateTime unlockedAt)
        {
            AchievementId = achievementId;
            UnlockedAt = unlockedAt;
        }
    }
}
