using FinanceMath.Application.Content.Contents.Dtos;
using FinanceMath.Application.Gamification.Achievements.Dtos;
using FinanceMath.Application.Gamification.Profiles.Dtos;

namespace FinanceMath.Application.Content.Exercises.Dtos
{
    public class CompleteExerciseResponseDto
    {
        public Guid ExerciseId { get; set; }
        public bool IsCorrect { get; set; }
        public bool UsedHint { get; set; }
        public string? Explanation { get; set; }
        public bool AlreadyCompleted { get; set; }
        public RewardDto Reward { get; set; } = new();
        public GamificationProfileSummaryDto Profile { get; set; } = new();
        public ICollection<UserAchievementDto> AchievementsUnlocked { get; set; } = Array.Empty<UserAchievementDto>();
        public ICollection<RecommendedContentDto> NextRecommended { get; set; } = Array.Empty<RecommendedContentDto>();
        public DateTime CompletedAtUtc { get; set; }
    }
}
