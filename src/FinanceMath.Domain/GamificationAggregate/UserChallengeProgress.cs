namespace FinanceMath.Domain.GamificationAggregate
{
    public class UserChallengeProgress
    {
        public virtual GamificationProfile GamificationProfile { get; protected set; }
        public virtual Challenge Challenge { get; protected set; }

        public virtual int CurrentProgress { get; protected set; }
        public virtual int TargetProgress { get; protected set; }
        public virtual bool IsCompleted { get; protected set; }

        public virtual DateTime StartedAt { get; protected set; }
        public virtual DateTime? CompletedAt { get; protected set; }

        protected UserChallengeProgress() { }

        public UserChallengeProgress(GamificationProfile profile, Challenge challenge)
        {
            GamificationProfile = profile;
            Challenge = challenge;
            TargetProgress = challenge.Target;
            CurrentProgress = 0;
            IsCompleted = false;
            StartedAt = DateTime.UtcNow;
        }

        public virtual void IncrementProgress(int amount = 1)
        {
            if (IsCompleted) return;

            CurrentProgress += amount;
            if (CurrentProgress >= TargetProgress)
            {
                CurrentProgress = TargetProgress;
                IsCompleted = true;
                CompletedAt = DateTime.UtcNow;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is not UserChallengeProgress other) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(GamificationProfile?.Id, other.GamificationProfile?.Id)
                && Equals(Challenge?.Id, other.Challenge?.Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (GamificationProfile?.Id.GetHashCode() ?? 0);
                hash = hash * 23 + (Challenge?.Id.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
