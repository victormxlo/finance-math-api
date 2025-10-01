using FinanceMath.Domain.ContentAggregate;

namespace FinanceMath.Domain.GamificationAggregate
{
    public class UserExerciseProgress
    {
        public virtual GamificationProfile GamificationProfile { get; protected set; }
        public virtual Exercise Exercise { get; protected set; }
        public virtual DateTime CompletedAt { get; protected set; }

        protected UserExerciseProgress() { }

        public UserExerciseProgress(GamificationProfile gamificationProfile, Exercise exercise, DateTime completedAt)
        {
            GamificationProfile = gamificationProfile;
            Exercise = exercise;
            CompletedAt = completedAt;
        }

        public override bool Equals(object obj)
        {
            if (obj is not UserExerciseProgress other) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(GamificationProfile?.Id, other.GamificationProfile?.Id)
                && Equals(Exercise?.Id, other.Exercise?.Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (GamificationProfile?.Id.GetHashCode() ?? 0);
                hash = hash * 23 + (Exercise?.Id.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
