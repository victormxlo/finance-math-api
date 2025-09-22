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

        public virtual ICollection<AchievementProgress> Achievements { get; protected set; } = new List<AchievementProgress>();

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
                Achievements.Add(new AchievementProgress(this, achievement, DateTime.UtcNow));
        }
    }
}
