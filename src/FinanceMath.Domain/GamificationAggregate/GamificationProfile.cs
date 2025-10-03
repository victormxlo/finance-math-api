using FinanceMath.Domain.ContentAggregate;
using FinanceMath.Domain.Users.Entities;

namespace FinanceMath.Domain.GamificationAggregate
{
    public class GamificationProfile
    {
        public virtual Guid Id { get; protected set; }
        public virtual User User { get; protected set; }

        public virtual int ExperiencePoints { get; protected set; }
        public virtual int VirtualCurrency { get; protected set; }
        public virtual Level Level { get; protected set; }
        public virtual int CurrentStreakDays { get; protected set; }
        public virtual DateTime? LastActivityDate { get; protected set; }

        public virtual ICollection<UserExerciseProgress> CompletedExercises { get; protected set; } = new List<UserExerciseProgress>();
        public virtual ICollection<UserContentProgress> CompletedContents { get; protected set; } = new List<UserContentProgress>();
        public virtual ICollection<UserAchievementProgress> Achievements { get; protected set; } = new List<UserAchievementProgress>();
        public virtual ICollection<UserChallengeProgress> ChallengeProgresses { get; protected set; } = new List<UserChallengeProgress>();

        protected GamificationProfile() { }

        public GamificationProfile(User user, Level level)
        {
            Id = Guid.NewGuid();
            User = user;
            ExperiencePoints = 0;
            VirtualCurrency = 0;
            Level = level;
            CurrentStreakDays = 0;
            LastActivityDate = null;
        }

        public virtual void AddExperience(int experiencePoints)
            => ExperiencePoints += experiencePoints;

        public virtual void AddVirtualCurrency(int virtualCurrencyAmount)
            => VirtualCurrency += virtualCurrencyAmount;

        public virtual void UpdateLevel(Level level)
            => Level = level;

        public virtual void UpdateStreak(DateTime activityDate)
        {
            if (LastActivityDate.HasValue && activityDate.Date == LastActivityDate.Value.AddDays(1).Date)
                CurrentStreakDays++;
            else if (LastActivityDate == null || activityDate.Date > LastActivityDate.Value.AddDays(1).Date)
                CurrentStreakDays = 1;

            LastActivityDate = activityDate;
        }

        public virtual void AddAchievement(Achievement achievement)
        {
            if (!Achievements.Any(a => a.Achievement.Id == achievement.Id))
                Achievements.Add(new UserAchievementProgress(this, achievement, DateTime.UtcNow));
        }

        public virtual bool HasCompletedExercise(Exercise exercise)
        {
            return CompletedExercises.Any(e => e.Exercise.Id == exercise.Id);
        }

        public virtual void MarkExerciseCompleted(Exercise exercise, DateTime completedAt)
        {
            if (!HasCompletedExercise(exercise))
                CompletedExercises.Add(new UserExerciseProgress(this, exercise, completedAt));
        }

        public virtual bool HasCompletedContent(Content content)
        {
            return CompletedContents.Any(c => c.Content.Id == content.Id);
        }

        public virtual void MarkContentCompleted(Content content, DateTime completedAt)
        {
            if (!HasCompletedContent(content))
                CompletedContents.Add(new UserContentProgress(this, content, completedAt));
        }

        public virtual UserChallengeProgress GetOrCreateChallengeProgress(Challenge challenge)
        {
            var progress = ChallengeProgresses.FirstOrDefault(p => p.Challenge.Id == challenge.Id);

            if (progress == null)
            {
                progress = new UserChallengeProgress(this, challenge);
                ChallengeProgresses.Add(progress);
            }

            return progress;
        }

        public virtual bool HasCompletedChallenge(Guid challengeId)
            => ChallengeProgresses.Any(cp => cp.Challenge.Id == challengeId && cp.IsCompleted);
    }
}
