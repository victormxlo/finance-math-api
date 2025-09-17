namespace FinanceMath.Domain.GamificationAggregate
{
    public class AchievementProgress
    {
        public virtual GamificationProfile GamificationProfile { get; protected set; }
        public virtual Achievement Achievement { get; protected set; }
        public virtual DateTime UnlockedAt { get; protected set; }

        protected AchievementProgress() { }

        public AchievementProgress(GamificationProfile gamificationProfile, Achievement achievement, DateTime unlockedAt)
        {
            GamificationProfile = gamificationProfile;
            Achievement = achievement;
            UnlockedAt = unlockedAt;
        }
    }
}
