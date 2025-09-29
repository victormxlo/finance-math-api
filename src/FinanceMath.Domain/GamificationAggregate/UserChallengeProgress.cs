namespace FinanceMath.Domain.GamificationAggregate
{
    public class UserChallengeProgress
    {
        public virtual GamificationProfile GamificationProfile { get; protected set; }
        public virtual Challenge Challenge { get; protected set; }
        public virtual DateTime CompletedAt { get; protected set; }

        protected UserChallengeProgress() { }

        public UserChallengeProgress(GamificationProfile gamificationProfile, Challenge challenge, DateTime completedAt)
        {
            GamificationProfile = gamificationProfile;
            Challenge = challenge;
            CompletedAt = completedAt;
        }
    }
}
