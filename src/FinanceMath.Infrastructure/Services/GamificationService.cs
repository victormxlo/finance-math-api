using AutoMapper;
using FinanceMath.Application.Content.Contents.Dtos;
using FinanceMath.Application.Content.Exercises.Dtos;
using FinanceMath.Application.Gamification.Achievements.Dtos;
using FinanceMath.Application.Gamification.Challenges.Dtos;
using FinanceMath.Application.Gamification.Leaderboards.Dtos;
using FinanceMath.Application.Gamification.Profiles.Dtos;
using FinanceMath.Application.Interfaces;
using FinanceMath.Application.Services;
using FinanceMath.Application.Settings;
using FinanceMath.Domain.ContentAggregate;
using FinanceMath.Domain.GamificationAggregate;
using FinanceMath.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FinanceMath.Infrastructure.Services
{
    public class GamificationService : IGamificationService
    {
        private readonly IGamificationProfileRepository _profileRepository;
        private readonly ILevelRepository _levelRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IAchievementRepository _achievementRepository;
        private readonly IChallengeRepository _challengeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRecommendationService _recommendationService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly GamificationSettings _settings;

        public GamificationService(
            IGamificationProfileRepository profileRepository,
            ILevelRepository levelRepository,
            IContentRepository contentRepository,
            IExerciseRepository exerciseRepository,
            IAchievementRepository achievementRepository,
            IChallengeRepository challengeRepository,
            IUserRepository userRepository,
            IRecommendationService recommendationService,
            IMediator mediator,
            IMapper mapper,
            ILogger<GamificationService> logger,
            IOptions<GamificationSettings> settings)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            _levelRepository = levelRepository ?? throw new ArgumentNullException(nameof(levelRepository));
            _contentRepository = contentRepository ?? throw new ArgumentNullException(nameof(contentRepository));
            _exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
            _achievementRepository = achievementRepository ?? throw new ArgumentNullException(nameof(achievementRepository));
            _challengeRepository = challengeRepository ?? throw new ArgumentNullException(nameof(challengeRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _recommendationService = recommendationService ?? throw new ArgumentNullException(nameof(recommendationService));
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task EnsureProfileExistsAsync(Guid userId)
        {
            var profile = await _profileRepository.GetByUserIdAsync(userId);
            var level = await _levelRepository.GetByIdAsync(1);
            if (profile == null)
            {
                var user = await _userRepository.GetByIdAsync(userId)
                    ?? throw new InvalidOperationException($"User {userId} not found when creating gamification profile.");

                profile = new GamificationProfile(user, level);
                await _profileRepository.AddAsync(profile);
                _logger.LogInformation("Created gamification profile for user {UserId}", userId);
            }
        }

        public async Task<GamificationProfileDto> GetProfileAsync(Guid userId)
        {
            var profile = await _profileRepository.GetByUserIdAsync(userId)
                          ?? throw new InvalidOperationException($"Gamification profile for user {userId} not found.");

            return _mapper.Map<GamificationProfileDto>(profile);
        }

        public async Task AwardExperienceAsync(Guid userId, int experiencePoints, string? reason = null)
        {
            if (experiencePoints <= 0) throw new ArgumentException("experiencePoints must be positive", nameof(experiencePoints));

            var profile = await EnsureAndGetProfileAsync(userId);

            profile.AddExperience(experiencePoints);
            profile.UpdateStreak(DateTime.UtcNow);

            await CheckLevelUpAsync(profile);

            await _profileRepository.UpdateAsync(profile);

            _logger.LogInformation("Awarded {Xp} XP to user {UserId}. Reason: {Reason}", experiencePoints, userId, reason);
        }

        public async Task AwardVirtualCurrencyAsync(Guid userId, int amount, string? reason = null)
        {
            if (amount == 0) return;
            var profile = await EnsureAndGetProfileAsync(userId);

            profile.AddVirtualCurrency(amount);
            await _profileRepository.UpdateAsync(profile);

            _logger.LogInformation("Awarded {Amount} virtual currency to user {UserId}. Reason: {Reason}", amount, userId, reason);
        }

        public async Task<CompleteContentResponseDto> CompleteContentAsync(
            Guid userId,
            Guid contentId)
        {
            var profile = await _profileRepository.GetByUserIdAsync(userId);
            if (profile == null) throw new InvalidOperationException($"Gamification profile not found for user {userId}");

            var content = await _contentRepository.GetByIdAsync(contentId);
            if (content == null) throw new InvalidOperationException($"Content {contentId} not found");

            var now = DateTime.UtcNow;
            var alreadyCompleted = profile.HasCompletedContent(content);

            int xpAwarded = 0;
            int currencyAwarded = 0;

            if (!alreadyCompleted)
            {
                xpAwarded = _settings.XpPerContent;
                currencyAwarded = _settings.VirtualCurrencyPerContent;
                profile.AddExperience(xpAwarded);
                profile.AddVirtualCurrency(currencyAwarded);

                profile.MarkContentCompleted(content, now);
            }
            else
            {
                xpAwarded = 0;
                currencyAwarded = 0;
            }

            profile.UpdateStreak(now);

            var newLevel = await GetLevelForXpAsync(profile.ExperiencePoints);
            if (newLevel != null && newLevel.Id != profile.Level.Id)
                profile.UpdateLevel(newLevel);

            var newlyUnlocked = await EvaluateAndApplyAchievementsAsync(profile, null, true);

            await _profileRepository.UpdateAsync(profile);

            var dto = new CompleteContentResponseDto
            {
                ContentId = contentId,
                ModuleCompleted = content.IsLastInModule,
                CompletedAtUtc = now,
                Reward = new RewardDto { XpAwarded = xpAwarded, VirtualCurrencyAwarded = currencyAwarded },
                Profile = MapProfileToSummary(profile),
                AchievementsUnlocked = newlyUnlocked.Select(a => new UserAchievementDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ExperienceReward = a.ExperienceReward,
                    VirtualCurrencyReward = a.VirtualCurrencyReward,
                    UnlockedAt = DateTime.UtcNow
                }).ToList()
            };

            if (_recommendationService != null)
            {
                var recommendations = await _recommendationService.GetRecommendationsAsync(profile.User.Id);
                dto.NextRecommended = recommendations.Items.ToList();
            }

            _logger.LogInformation("User {UserId} completed content {ContentId}. xp={Xp}", userId, contentId, xpAwarded);

            return dto;
        }


        public async Task<CompleteExerciseResponseDto> CompleteExerciseAsync(
            Guid userId,
            Guid exerciseId,
            Guid? selectedOptionId,
            bool usedHint)
        {
            var profile = await _profileRepository.GetByUserIdAsync(userId);
            if (profile == null) throw new InvalidOperationException($"Gamification profile not found for user {userId}");

            var exercise = await _exerciseRepository.GetByIdAsync(exerciseId);
            if (exercise == null) throw new InvalidOperationException($"Exercise {exerciseId} not found");

            bool isCorrect = EvaluateExerciseAnswer(exercise, selectedOptionId);
            string explanation = exercise.Explanation;

            var now = DateTime.UtcNow;
            var alreadyCompleted = profile.HasCompletedExercise(exercise);

            int xpAwarded;
            int currencyAwarded;

            if (!alreadyCompleted)
            {
                if (isCorrect)
                {
                    xpAwarded = _settings.XpPerExercise;
                    if (!usedHint) xpAwarded += _settings.XpPerExerciseNoHintBonus;
                    currencyAwarded = _settings.VirtualCurrencyPerExercise;
                }
                else
                {
                    xpAwarded = _settings.XpPerExerciseIncorrect;
                    currencyAwarded = 0;
                }

                profile.AddExperience(xpAwarded);
                profile.AddVirtualCurrency(currencyAwarded);

                profile.MarkExerciseCompleted(exercise, now);
            }
            else
            {
                xpAwarded = 0;
                currencyAwarded = 0;
            }

            profile.UpdateStreak(now);

            var newLevel = await GetLevelForXpAsync(profile.ExperiencePoints);
            if (newLevel != null && newLevel.Id != profile.Level.Id)
                profile.UpdateLevel(newLevel);

            var newlyUnlocked = await EvaluateAndApplyAchievementsAsync(profile, exercise, isCorrect);

            await _profileRepository.UpdateAsync(profile);

            var dto = new CompleteExerciseResponseDto
            {
                ExerciseId = exerciseId,
                IsCorrect = isCorrect,
                UsedHint = usedHint,
                Explanation = explanation,
                AlreadyCompleted = alreadyCompleted,
                CompletedAtUtc = now,
                Reward = new RewardDto { XpAwarded = xpAwarded, VirtualCurrencyAwarded = currencyAwarded },
                Profile = MapProfileToSummary(profile),
                AchievementsUnlocked = newlyUnlocked.Select(a => new UserAchievementDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ExperienceReward = a.ExperienceReward,
                    VirtualCurrencyReward = a.VirtualCurrencyReward,
                    UnlockedAt = DateTime.UtcNow
                }).ToList()
            };

            if (_recommendationService != null)
            {
                var recommendations = await _recommendationService.GetRecommendationsAsync(profile.User.Id);
                dto.NextRecommended = recommendations.Items.ToList();
            }

            _logger.LogInformation("User {UserId} completed exercise {ExerciseId}. correct={IsCorrect}, xp={Xp}",
                userId, exerciseId, isCorrect, xpAwarded);

            return dto;
        }

        public async Task<CompleteChallengeResponseDto> CompleteChallengeAsync(
            Guid userId,
            Guid challengeId)
        {
            var profile = await _profileRepository.GetByUserIdAsync(userId);
            if (profile == null) throw new InvalidOperationException("Profile not found");

            var challenge = await _challengeRepository.GetByIdAsync(challengeId);
            if (challenge == null) throw new InvalidOperationException("Challenge not found");

            var alreadyCompleted = profile.HasCompletedChallenge(challengeId);
            int xpAwarded = 0, currencyAwarded = 0;

            if (!alreadyCompleted)
            {
                xpAwarded = _settings.XpPerChallenge;
                currencyAwarded = _settings.VirtualCurrencyPerChallenge;

                profile.AddExperience(xpAwarded);
                profile.AddVirtualCurrency(currencyAwarded);

                profile.MarkChallengeCompleted(challenge, DateTime.UtcNow);

                await ApplyAchievementsByCriteriaInternalAsync(profile, "challenge_completed");

                await _profileRepository.UpdateAsync(profile);
            }

            return new CompleteChallengeResponseDto
            {
                ChallengeId = challengeId,
                XpAwarded = xpAwarded,
                VirtualCurrencyAwarded = currencyAwarded,
                TotalXp = profile.ExperiencePoints,
                LevelId = profile.Level.Id,
                LevelName = profile.Level.Name
            };
        }

        public async Task ApplyAchievementsByCriteriaAsync(Guid userId, string criteriaKey)
        {
            if (string.IsNullOrWhiteSpace(criteriaKey)) throw new ArgumentException("criteriaKey required", nameof(criteriaKey));

            var profile = await EnsureAndGetProfileAsync(userId);

            await ApplyAchievementsByCriteriaInternalAsync(profile, criteriaKey);

            await _profileRepository.UpdateAsync(profile);
        }

        public async Task UnlockAchievementAsync(Guid userId, Guid achievementId)
        {
            var profile = await EnsureAndGetProfileAsync(userId);
            var achievement = await _achievementRepository.GetByIdAsync(achievementId)
                ?? throw new InvalidOperationException($"Achievement {achievementId} not found.");

            profile.AddAchievement(achievement);
            profile.AddExperience(achievement.ExperienceReward);
            profile.AddVirtualCurrency(achievement.VirtualCurrencyReward);

            await CheckLevelUpAsync(profile);
            await _profileRepository.UpdateAsync(profile);

            _logger.LogInformation("Unlocked achievement {AchievementId} for user {UserId}", achievementId, userId);
        }

        public async Task<ICollection<LeaderboardEntryDto>> GetLeaderboardAsync(int? top = null)
        {
            var topAmount = top ?? 50;

            var topProfiles = await _profileRepository.GetTopByExperienceAsync(topAmount);

            var result = new List<LeaderboardEntryDto>(topProfiles.Count());
            foreach (var p in topProfiles)
            {
                result.Add(new LeaderboardEntryDto
                {
                    UserId = p.User.Id,
                    Username = p.User.Username,
                    LevelId = p.Level.Id,
                    LevelName = p.Level.Name,
                    ExperiencePoints = p.ExperiencePoints
                });
            }

            return result;
        }

        public async Task<ICollection<ChallengeDto>> GetActiveChallengesAsync()
        {
            var challenges = await _challengeRepository.GetActivesAsync();

            return _mapper.Map<ICollection<ChallengeDto>>(challenges);
        }

        #region Helpers

        private async Task<GamificationProfile> EnsureAndGetProfileAsync(Guid userId)
        {
            var profile = await _profileRepository.GetByUserIdAsync(userId);
            if (profile == null)
            {
                var user = await _userRepository.GetByIdAsync(userId)
                    ?? throw new InvalidOperationException($"User {userId} not found when creating gamification profile.");

                var level = await _levelRepository.GetInitialLevelAsync();

                profile = new GamificationProfile(user, level);
                await _profileRepository.AddAsync(profile);
                _logger.LogInformation("Created gamification profile for user {UserId}", userId);
            }

            return profile;
        }

        private async Task ApplyAchievementsByCriteriaInternalAsync(GamificationProfile profile, string criteriaKey)
        {
            if (string.IsNullOrWhiteSpace(criteriaKey)) return;

            var achievements = await _achievementRepository.GetByCriteriaAsync(criteriaKey);
            if (achievements == null) return;

            foreach (var ach in achievements)
            {
                var already = profile.Achievements.Any(a => a.Achievement.Id == ach.Id);
                if (already) continue;

                profile.AddAchievement(ach);
                if (ach.ExperienceReward > 0)
                    profile.AddExperience(ach.ExperienceReward);
                if (ach.VirtualCurrencyReward != 0)
                    profile.AddVirtualCurrency(ach.VirtualCurrencyReward);

                _logger.LogInformation("Applied achievement {AchievementId} to user {UserId} via criteria {Criteria}", ach.Id, profile.User.Id, criteriaKey);
            }
        }

        private async Task CheckLevelUpAsync(GamificationProfile profile)
        {
            var levels = (await _levelRepository.GetAllAsync()).OrderBy(l => l.ThresholdExperience).ToList();
            if (!levels.Any()) return;

            var newLevel = levels.LastOrDefault(l => profile.ExperiencePoints >= l.ThresholdExperience);
            if (newLevel != null && newLevel.Id != profile.Level.Id)
            {
                var previousLevel = profile.Level.Id;
                profile.UpdateLevel(newLevel);

                _logger.LogInformation("User {UserId} leveled up from {OldLevel} to {NewLevel}", profile.User.Id, previousLevel, newLevel.Id);
            }
        }

        private async Task<Level?> GetLevelForXpAsync(int xp)
        {
            var levels = (await _levelRepository.GetAllAsync()).OrderBy(l => l.ThresholdExperience).ToList();

            if (!levels.Any()) return null;

            var level = levels.LastOrDefault(l => xp >= l.ThresholdExperience) ?? levels.First();

            return level;
        }

        private bool EvaluateExerciseAnswer(Exercise exercise, Guid? selectedOptionId)
        {
            if (selectedOptionId == null) return false;

            return exercise.Options?.Any(o => o.Id == selectedOptionId && o.IsCorrect) ?? false;
        }

        private async Task<IEnumerable<Achievement>> EvaluateAndApplyAchievementsAsync(
            GamificationProfile profile,
            Exercise? exerciseContext = null,
            bool lastActionWasCorrect = false)
        {
            var unlocked = new List<Achievement>();
            var allAchievements = await _achievementRepository.GetAllAsync();

            foreach (var achievement in allAchievements)
            {
                if (profile.Achievements.Any(ap => ap.Achievement.Id == achievement.Id))
                    continue;

                var key = achievement.CriteriaKey;
                if (string.IsNullOrWhiteSpace(key))
                    continue;

                bool shouldUnlock = false;

                if (key.StartsWith("complete_exercise:", StringComparison.OrdinalIgnoreCase))
                {
                    if (Guid.TryParse(key.Split(':', 2)[1], out var targetExerciseId))
                    {
                        var exercise = await _exerciseRepository.GetByIdAsync(targetExerciseId);
                        if (exercise is null) throw new InvalidOperationException("Exercise not found.");
                        shouldUnlock = profile.HasCompletedExercise(exercise);
                    }
                }
                else if (key.StartsWith("complete_content:", StringComparison.OrdinalIgnoreCase))
                {
                    if (Guid.TryParse(key.Split(':', 2)[1], out var targetContentId))
                    {
                        var content = await _contentRepository.GetByIdAsync(targetContentId);
                        if (content is null) throw new InvalidOperationException("Content not found.");
                        shouldUnlock = profile.HasCompletedContent(content);
                    }
                }
                else if (key.StartsWith("complete_n_exercises:", StringComparison.OrdinalIgnoreCase))
                {
                    if (int.TryParse(key.Split(':', 2)[1], out var n))
                    {
                        shouldUnlock = profile.CompletedExercises.Count >= n;
                    }
                }
                else if (key.StartsWith("streak_days:", StringComparison.OrdinalIgnoreCase))
                {
                    if (int.TryParse(key.Split(':', 2)[1], out var days))
                    {
                        shouldUnlock = profile.CurrentStreakDays >= days;
                    }
                }
                else if (key.Equals("first_exercise_completed", StringComparison.OrdinalIgnoreCase))
                {
                    shouldUnlock = profile.CompletedExercises.Count >= 1;
                }

                if (shouldUnlock)
                {
                    profile.AddAchievement(achievement);

                    if (achievement.ExperienceReward > 0)
                        profile.AddExperience(achievement.ExperienceReward);

                    if (achievement.VirtualCurrencyReward != 0)
                        profile.AddVirtualCurrency(achievement.VirtualCurrencyReward);

                    unlocked.Add(achievement);
                }
            }

            return unlocked;
        }

        private GamificationProfileSummaryDto MapProfileToSummary(GamificationProfile profile)
        {
            return new GamificationProfileSummaryDto
            {
                UserId = profile.User.Id,
                ExperiencePoints = profile.ExperiencePoints,
                VirtualCurrency = profile.VirtualCurrency,
                LevelId = profile.Level?.Id ?? 0,
                LevelName = profile.Level?.Name ?? string.Empty,
                CurrentStreakDays = profile.CurrentStreakDays,
                LastActivityDate = profile.LastActivityDate
            };
        }

        #endregion
    }
}
