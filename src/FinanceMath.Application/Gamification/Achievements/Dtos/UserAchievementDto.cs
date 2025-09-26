namespace FinanceMath.Application.Gamification.Achievements.Dtos
{
    public class UserAchievementDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public int ExperienceReward { get; set; }
        public int VirtualCurrencyReward { get; set; }
        public DateTime UnlockedAt { get; set; }
    }
}
