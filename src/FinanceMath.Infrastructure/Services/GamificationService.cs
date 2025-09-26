using AutoMapper;
using FinanceMath.Application.Content.Contents.Dtos;
using FinanceMath.Application.Content.Exercises.Dtos;
using FinanceMath.Application.Gamification.Achievements.Dtos;
using FinanceMath.Application.Gamification.Challenges.Dtos;
using FinanceMath.Application.Gamification.Leaderboards.Dtos;
using FinanceMath.Application.Gamification.Profiles.Dtos;
using FinanceMath.Application.Interfaces;
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

            return MapToDto(profile);
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
            if (amount == 0) return; // nothing to do
            var profile = await EnsureAndGetProfileAsync(userId);

            profile.AddVirtualCurrency(amount);
            await _profileRepository.UpdateAsync(profile);

            _logger.LogInformation("Awarded {Amount} virtual currency to user {UserId}. Reason: {Reason}", amount, userId, reason);
        }

        public async Task ProcessExerciseCompletedAsync(Guid userId, Guid exerciseId, bool isCorrect, bool usedHint, DateTime activityDateUtc)
        {
            var profile = await EnsureAndGetProfileAsync(userId);

            // determine xp
            int xp;
            if (isCorrect)
            {
                xp = _settings.XpPerExercise;
                if (!usedHint) xp += _settings.XpPerExerciseNoHintBonus;
            }
            else
            {
                xp = _settings.XpPerExerciseIncorrect;
            }

            profile.AddExperience(xp);
            profile.UpdateStreak(activityDateUtc);

            // persist changes & level/achievements
            await ApplyAchievementsByCriteriaInternalAsync(profile, $"exercise:{exerciseId}:correct:{isCorrect}");
            await CheckLevelUpAsync(profile);
            await _profileRepository.UpdateAsync(profile);

            _logger.LogInformation("Processed exercise completed for user {UserId} exercise {ExerciseId} isCorrect={IsCorrect} awardedXp={Xp}",
                userId, exerciseId, isCorrect, xp);
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

            var newlyUnlocked = await EvaluateAndApplyAchievementsAsync(profile, null, true); // pass exercise null

            await _profileRepository.UpdateAsync(profile);

            var dto = new CompleteContentResponseDto
            {
                ContentId = contentId,
                ModuleCompleted = content.IsLastInModule, // or however you determine
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

            //if (_recommendationService != null)
            //    dto.NextRecommended = await _recommendationService.RecommendNextForUserAsync(profile.User.Id, ct);

            _logger.LogInformation("User {UserId} completed content {ContentId}. xp={Xp}", userId, contentId, xpAwarded);

            return dto;
        }


        public async Task<CompleteExerciseResponseDto> CompleteExerciseAsync(
            Guid userId,
            Guid exerciseId,
            Guid? selectedOptionId,
            bool usedHint)
        {
            // 1. loads
            var profile = await _profileRepository.GetByUserIdAsync(userId);
            if (profile == null) throw new InvalidOperationException($"Gamification profile not found for user {userId}");

            var exercise = await _exerciseRepository.GetByIdAsync(exerciseId);
            if (exercise == null) throw new InvalidOperationException($"Exercise {exerciseId} not found");

            // evaluate correctness
            bool isCorrect = EvaluateExerciseAnswer(exercise, selectedOptionId);
            string explanation = exercise.Explanation;

            var now = DateTime.UtcNow;
            var alreadyCompleted = profile.HasCompletedExercise(exercise);

            // compute XP & currency awarding logic
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

                // agora marcamos com ENTIDADE Exercise (navegação)
                profile.MarkExerciseCompleted(exercise, now);
            }
            else
            {
                xpAwarded = 0;
                currencyAwarded = 0;
            }

            // always update streak on completion (policy: update even if incorrect)
            profile.UpdateStreak(now);

            // compute level based on new total XP:
            var newLevel = await GetLevelForXpAsync(profile.ExperiencePoints);
            if (newLevel != null && newLevel.Id != profile.Level.Id)
                profile.UpdateLevel(newLevel);

            // Evaluate achievements: collect newly unlocked achievements
            var newlyUnlocked = await EvaluateAndApplyAchievementsAsync(profile, exercise, isCorrect);

            await _profileRepository.UpdateAsync(profile);

            // build DTO
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

            // TBI
            //if (_recommendationService != null)
            //{
            //    dto.NextRecommended = await _recommendationService.RecommendNextForUserAsync(profile.User.Id, ct);
            //}

            _logger.LogInformation("User {UserId} completed exercise {ExerciseId}. correct={IsCorrect}, xp={Xp}",
                userId, exerciseId, isCorrect, xpAwarded);

            return dto;
        }

        public async Task ProcessContentCompletedAsync(Guid userId, Guid contentId, DateTime activityDateUtc)
        {
            var profile = await EnsureAndGetProfileAsync(userId);

            profile.AddExperience(_settings.XpPerContent);
            profile.UpdateStreak(activityDateUtc);

            await ApplyAchievementsByCriteriaInternalAsync(profile, $"content:{contentId}:completed");
            await CheckLevelUpAsync(profile);
            await _profileRepository.UpdateAsync(profile);

            _logger.LogInformation("Processed content completed for user {UserId} content {ContentId} awardedXp={Xp}",
                userId, contentId, _settings.XpPerContent);
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

            // add if not present
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

        public async Task<bool> ClaimChallengeRewardAsync(Guid userId, Guid challengeId)
        {
            var profile = await EnsureAndGetProfileAsync(userId);
            var challenge = await _challengeRepository.GetByIdAsync(challengeId)
                ?? throw new InvalidOperationException($"Challenge {challengeId} not found.");

            // validate active
            var now = DateTime.UtcNow;
            if (challenge.StartDate > now || challenge.EndDate < now) return false;

            // Check if user already claimed (we need a persisted record; assuming AchievementProgress or challenge_claims table)
            // Basic implementation: use a criteria achievement to mark claim
            var claimCriteria = $"challenge_claimed:{challengeId}";
            var existingAchievements = await _achievementRepository.GetByCriteriaAsync(claimCriteria);
            // If there is a dedicated claim mechanism, prefer that; here we'll reuse achievements pattern:
            var alreadyClaimed = profile.Achievements.Any(ap => ap.Achievement.CriteriaKey == claimCriteria);
            if (alreadyClaimed) return false;

            // award rewards
            profile.AddExperience(challenge.ExperienceReward);
            profile.AddVirtualCurrency(challenge.VirtualCurrencyReward);

            // mark as claimed: create or use a special achievement if exists
            var claimAchievements = await _achievementRepository.GetByCriteriaAsync(claimCriteria);
            var claimAchievement = claimAchievements.FirstOrDefault();
            if (claimAchievement != null)
            {
                profile.AddAchievement(claimAchievement);
            }

            await CheckLevelUpAsync(profile);
            await _profileRepository.UpdateAsync(profile);

            _logger.LogInformation("User {UserId} claimed challenge {ChallengeId} rewards.", userId, challengeId);
            return true;
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

            // find highest level for which profile.ExperiencePoints >= threshold
            var newLevel = levels.LastOrDefault(l => profile.ExperiencePoints >= l.ThresholdExperience);
            if (newLevel != null && newLevel.Id != profile.Level.Id)
            {
                var previousLevel = profile.Level.Id;
                profile.UpdateLevel(newLevel);

                _logger.LogInformation("User {UserId} leveled up from {OldLevel} to {NewLevel}", profile.User.Id, previousLevel, newLevel.Id);
            }
        }

        private GamificationProfileDto MapToDto(GamificationProfile profile)
        {
            return new GamificationProfileDto
            {
                UserId = profile.User.Id,
                ExperiencePoints = profile.ExperiencePoints,
                VirtualCurrency = profile.VirtualCurrency,
                LevelId = profile.Level.Id,
                CurrentStreakDays = profile.CurrentStreakDays,
                LastActivityDate = profile.LastActivityDate,
                AchievementsIds = profile.Achievements.Select(a => a.Achievement.Id).ToList()
            };
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
            GamificationProfile profile, Exercise exerciseContext, bool lastActionWasCorrect)
        {
            var unlocked = new List<Achievement>();
            var allAchievements = await _achievementRepository.GetAllAsync();

            foreach (var achievement in allAchievements)
            {
                if (profile.Achievements.Any(ap => ap.Achievement.Id == achievement.Id))
                    continue;

                // simple criteria parsing
                var key = achievement.CriteriaKey ?? string.Empty;

                bool shouldUnlock = false;

                if (key.StartsWith("complete_exercise:"))
                {
                    var parts = key.Split(':', 2);
                    if (parts.Length == 2 && Guid.TryParse(parts[1], out var targetExerciseId))
                    {
                        var exercise = await _exerciseRepository.GetByIdAsync(targetExerciseId);

                        if (exercise != null && profile.HasCompletedExercise(exercise)) shouldUnlock = true;
                    }
                }
                else if (key.StartsWith("complete_content:"))
                {
                    var parts = key.Split(':', 2);
                    if (parts.Length == 2 && Guid.TryParse(parts[1], out var targetContentId))
                    {
                        var content = await _contentRepository.GetByIdAsync(targetContentId);

                        if (content != null && profile.HasCompletedContent(content)) shouldUnlock = true;
                    }
                }
                else if (key.StartsWith("complete_n_exercises:"))
                {
                    var parts = key.Split(':', 2);
                    if (parts.Length == 2 && int.TryParse(parts[1], out var n))
                    {
                        if (profile.CompletedExercises.Count >= n) shouldUnlock = true;
                    }
                }
                else if (key.StartsWith("streak_days:"))
                {
                    var parts = key.Split(':', 2);
                    if (parts.Length == 2 && int.TryParse(parts[1], out var days))
                    {
                        if (profile.CurrentStreakDays >= days) shouldUnlock = true;
                    }
                }
                else if (key == "first_exercise_completed")
                {
                    if (profile.CompletedExercises.Count >= 1) shouldUnlock = true;
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
