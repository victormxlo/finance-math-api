namespace FinanceMath.Application.Gamification.Profiles.Dtos
{
    public class GamificationProfileSummaryDto
    {
        public Guid UserId { get; set; }
        public int ExperiencePoints { get; set; }
        public int VirtualCurrency { get; set; }
        public int LevelId { get; set; }
        public string LevelName { get; set; } = default!;
        public int CurrentStreakDays { get; set; }
        public DateTime? LastActivityDate { get; set; }
    }
}
