namespace FinanceMath.Application.Gamification.Profiles.Dtos
{
    public class GamificationProfileDto
    {
        public Guid UserId { get; set; }
        public int ExperiencePoints { get; set; }
        public int VirtualCurrency { get; set; }
        public int LevelId { get; set; }
        public int CurrentStreakDays { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public ICollection<Guid> AchievementsIds { get; set; }
        public ICollection<Guid> ChallengesIds { get; set; }
    }
}
