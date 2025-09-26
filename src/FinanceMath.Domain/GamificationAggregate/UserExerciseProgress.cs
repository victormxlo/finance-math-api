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
    }
}
