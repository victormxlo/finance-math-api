using FinanceMath.Application.Recommendations.Dtos;
using FinanceMath.Domain.Repositories;

namespace FinanceMath.Application.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IGamificationProfileRepository _profileRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IContentRepository _contentRepository;

        public RecommendationService(
            IGamificationProfileRepository profileRepository,
            IExerciseRepository exerciseRepository,
            IContentRepository contentRepository)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            _exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
            _contentRepository = contentRepository ?? throw new ArgumentNullException(nameof(contentRepository));
        }

        public async Task<RecommendationResultDto> GetRecommendationsAsync(Guid userId, int maxItems = 5)
        {
            var profile = await _profileRepository.GetByUserIdAsync(userId);
            if (profile == null)
                throw new InvalidOperationException($"Gamification profile not found for user {userId}");

            var allExercises = await _exerciseRepository.GetAllAsync();
            var allContents = await _contentRepository.GetAllAsync();

            var notCompletedExercises = allExercises
                .Where(e => !profile.HasCompletedExercise(e))
                .ToList();

            var notCompletedContents = allContents
                .Where(c => !profile.HasCompletedContent(c))
                .ToList();

            var recommendedExercises = notCompletedExercises
                .OrderBy(e => DifficultyDistance(e.Difficulty))
                .Take(maxItems / 2)
                .Select(e => new RecommendedItemDto
                {
                    Id = e.Id,
                    Title = e.Question,
                    Type = "exercise",
                    Difficulty = e.Difficulty
                });

            var recommendedContents = notCompletedContents
                .OrderBy(c => c.Category.Id)
                .Take(maxItems - recommendedExercises.Count())
                .Select(c => new RecommendedItemDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Type = "content",
                    Category = c.Category.Name
                });

            var items = recommendedExercises.Concat(recommendedContents).ToList();
            return new RecommendationResultDto(items);
        }

        private static int DifficultyDistance(string exerciseDifficulty)
        {
            int ToScore(string diff) => diff?.ToLowerInvariant() switch
            {
                "easy" => 1,
                "medium" => 2,
                "hard" => 3,
                _ => 2
            };

            return Math.Abs(ToScore(exerciseDifficulty));
        }
    }
}
