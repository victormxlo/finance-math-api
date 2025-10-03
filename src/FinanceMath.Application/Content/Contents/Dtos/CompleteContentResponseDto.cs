using FinanceMath.Application.Gamification.Achievements.Dtos;
using FinanceMath.Application.Gamification.Challenges.Dtos;
using FinanceMath.Application.Gamification.Profiles.Dtos;
using FinanceMath.Application.Recommendations.Dtos;

namespace FinanceMath.Application.Content.Contents.Dtos
{
    public class CompleteContentResponseDto
    {
        public Guid ContentId { get; set; }
        public bool ModuleCompleted { get; set; }
        public RewardDto Reward { get; set; } = new();
        public GamificationProfileSummaryDto Profile { get; set; } = new();
        public ICollection<UserAchievementDto> AchievementsUnlocked { get; set; } = Array.Empty<UserAchievementDto>();
        public DateTime CompletedAtUtc { get; set; }

        public ICollection<UserChallengeProgressDto> ChallengesProgress { get; set; } = new List<UserChallengeProgressDto>();

        public ICollection<RecommendedItemDto> NextRecommended { get; set; } = Array.Empty<RecommendedItemDto>();
    }
}
