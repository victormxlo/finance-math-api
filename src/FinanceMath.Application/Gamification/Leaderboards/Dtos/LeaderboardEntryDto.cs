namespace FinanceMath.Application.Gamification.Leaderboards.Dtos
{
    public class LeaderboardEntryDto
    {
        public Guid UserId { get; set; }
        public required string Username { get; set; }
        public int ExperiencePoints { get; set; }
        public int Rank { get; set; }
    }
}
