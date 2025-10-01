namespace FinanceMath.Domain.GamificationAggregate
{
    public class UserAchievementProgress
    {
        public virtual GamificationProfile GamificationProfile { get; protected set; }
        public virtual Achievement Achievement { get; protected set; }
        public virtual DateTime UnlockedAt { get; protected set; }

        protected UserAchievementProgress() { }

        public UserAchievementProgress(GamificationProfile gamificationProfile, Achievement achievement, DateTime unlockedAt)
        {
            GamificationProfile = gamificationProfile;
            Achievement = achievement;
            UnlockedAt = unlockedAt;
        }

        public override bool Equals(object obj)
        {
            if (obj is not UserAchievementProgress other) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(GamificationProfile?.Id, other.GamificationProfile?.Id)
                && Equals(Achievement?.Id, other.Achievement?.Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (GamificationProfile?.Id.GetHashCode() ?? 0);
                hash = hash * 23 + (Achievement?.Id.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
