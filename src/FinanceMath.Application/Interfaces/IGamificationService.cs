using FinanceMath.Application.Content.Contents.Dtos;
using FinanceMath.Application.Content.Exercises.Dtos;
using FinanceMath.Application.Gamification.Challenges.Dtos;
using FinanceMath.Application.Gamification.Leaderboards.Dtos;
using FinanceMath.Application.Gamification.Profiles.Dtos;

namespace FinanceMath.Application.Interfaces
{
    public interface IGamificationService
    {
        Task EnsureProfileExistsAsync(Guid userId);

        Task<GamificationProfileDto> GetProfileAsync(Guid userId);

        Task AwardExperienceAsync(Guid userId, int experiencePoints, string? reason = null);

        Task AwardVirtualCurrencyAsync(Guid userId, int amount, string? reason = null);

        Task ProcessExerciseCompletedAsync(Guid userId, Guid exerciseId, bool isCorrect, bool usedHint, DateTime activityDateUtc);

        Task<CompleteContentResponseDto> CompleteContentAsync(Guid userId, Guid contentId);

        Task<CompleteExerciseResponseDto> CompleteExerciseAsync(Guid userId, Guid exerciseId, Guid? selectedOptionId, bool usedHint);

        Task ProcessContentCompletedAsync(Guid userId, Guid contentId, DateTime activityDateUtc);

        Task ApplyAchievementsByCriteriaAsync(Guid userId, string criteriaKey);

        Task UnlockAchievementAsync(Guid userId, Guid achievementId);

        Task<ICollection<LeaderboardEntryDto>> GetLeaderboardAsync(int? top = null);

        Task<ICollection<ChallengeDto>> GetActiveChallengesAsync();

        Task<bool> ClaimChallengeRewardAsync(Guid userId, Guid challengeId);
    }
}
