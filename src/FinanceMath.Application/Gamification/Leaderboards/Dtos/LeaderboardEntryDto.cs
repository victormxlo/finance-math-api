namespace FinanceMath.Application.Gamification.Leaderboards.Dtos
{
    public class LeaderboardEntryDto
    {
        public Guid UserId { get; set; }
        public required string Username { get; set; }
        public int LevelId { get; set; }
        public required string LevelName { get; set; }
        public int ExperiencePoints { get; set; }
    }
}
