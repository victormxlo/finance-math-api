using ClosedXML.Excel;
using FinanceMath.Application.Interfaces;
using FinanceMath.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace FinanceMath.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly IGamificationProfileRepository _profileRepo;
        private readonly IUserRepository _userRepo;
        private readonly IChallengeRepository _challengeRepo;
        private readonly IAchievementRepository _achievementRepo;
        private readonly IUserChallengeProgressRepository _userChallengeProgressRepo;
        private readonly IUserExerciseProgressRepository _userExerciseProgressRepo;
        private readonly IUserContentProgressRepository _userContentProgressRepo;
        private readonly IUserAchievementProgressRepository _userAchievementProgressRepository;
        private readonly IExerciseRepository _exerciseRepo;
        private readonly IContentRepository _contentRepo;
        private readonly ILogger<ReportService> _logger;

        public ReportService(
            IGamificationProfileRepository profileRepo,
            IUserRepository userRepo,
            IChallengeRepository challengeRepo,
            IAchievementRepository achievementRepo,
            IUserChallengeProgressRepository userChallengeProgressRepo,
            IUserExerciseProgressRepository userExerciseProgressRepo,
            IUserContentProgressRepository userContentProgressRepo,
            IUserAchievementProgressRepository userAchievementProgressRepository,
            IExerciseRepository exerciseRepo,
            IContentRepository contentRepo,
            ILogger<ReportService> logger)
        {
            _profileRepo = profileRepo;
            _userRepo = userRepo;
            _challengeRepo = challengeRepo;
            _achievementRepo = achievementRepo;
            _userChallengeProgressRepo = userChallengeProgressRepo;
            _userExerciseProgressRepo = userExerciseProgressRepo;
            _userContentProgressRepo = userContentProgressRepo;
            _userAchievementProgressRepository = userAchievementProgressRepository;
            _exerciseRepo = exerciseRepo;
            _contentRepo = contentRepo;
            _logger = logger;
        }

        public async Task<byte[]> GenerateUserEngagementReportAsync(DateTime? lastActivitySince = null, CancellationToken cancellation = default)
        {
            var profiles = (await _profileRepo.GetAllAsync()).ToList();
            var users = (await _userRepo.GetAllAsync()).ToList();
            var userChallengeProgresses = (await _userChallengeProgressRepo.GetAllAsync()).ToList();
            var achievementProgresses = (await _userAchievementProgressRepository.GetAllAsync()).ToList();

            var totalUsers = users.Count;
            var activeSince = lastActivitySince ?? DateTime.UtcNow.AddDays(-7);

            var activeProfileIdsFromChallenge = userChallengeProgresses
                .Where(p => p.CompletedAt != default && p.CompletedAt >= activeSince)
                .Select(p => p.GamificationProfile.Id).Distinct();

            var activeProfileIdsFromAchievement = achievementProgresses
                .Where(ap => ap.UnlockedAt >= activeSince)
                .Select(ap => ap.GamificationProfile.Id).Distinct();

            var activeProfileIds = activeProfileIdsFromChallenge
                .Concat(activeProfileIdsFromAchievement)
                .Distinct()
                .ToList();

            var activeByProfileDate = profiles.Where(p => p.LastActivityDate.HasValue && p.LastActivityDate.Value >= activeSince).Select(p => p.Id);
            activeProfileIds = activeProfileIds.Concat(activeByProfileDate).Distinct().ToList();

            var totalActiveLast7Days = activeProfileIds.Count;

            var avgXp = profiles.Any() ? profiles.Average(p => p.ExperiencePoints) : 0;
            var avgCurrency = profiles.Any() ? profiles.Average(p => p.VirtualCurrency) : 0;

            var totalChallengesCompleted = userChallengeProgresses.Count(p => p.IsCompleted);
            var totalAchievementsUnlocked = achievementProgresses.Count();

            var top5 = profiles.OrderByDescending(p => p.ExperiencePoints)
                .Take(5)
                .Select(p =>
                {
                    var user = users.FirstOrDefault(u => u.Id == p.User.Id);
                    return new
                    {
                        Username = user?.Username ?? "(unknown)",
                        Level = p.Level?.Name ?? "",
                        Xp = p.ExperiencePoints,
                        VirtualCurrency = p.VirtualCurrency
                    };
                }).ToList();

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("User Engagement");

            int row = 1;
            ws.Cell(row, 1).Value = "User Engagement Report";
            ws.Cell(row, 1).Style.Font.Bold = true;
            row += 2;

            ws.Cell(row, 1).Value = "Total Users";
            ws.Cell(row, 2).Value = totalUsers;
            row++;

            ws.Cell(row, 1).Value = $"Users active since {activeSince:yyyy-MM-dd}";
            ws.Cell(row, 2).Value = totalActiveLast7Days;
            row++;

            ws.Cell(row, 1).Value = "Average XP";
            ws.Cell(row, 2).Value = avgXp;
            row++;

            ws.Cell(row, 1).Value = "Average Virtual Currency";
            ws.Cell(row, 2).Value = avgCurrency;
            row++;

            ws.Cell(row, 1).Value = "Total Challenges Completed";
            ws.Cell(row, 2).Value = totalChallengesCompleted;
            row++;

            ws.Cell(row, 1).Value = "Total Achievements Unlocked";
            ws.Cell(row, 2).Value = totalAchievementsUnlocked;
            row += 2;

            ws.Cell(row, 1).Value = "Top 5 Users by XP";
            ws.Cell(row, 1).Style.Font.Bold = true;
            row++;
            ws.Cell(row, 1).Value = "Username";
            ws.Cell(row, 2).Value = "Level";
            ws.Cell(row, 3).Value = "XP";
            ws.Cell(row, 4).Value = "Virtual Currency";
            ws.Row(row).Style.Font.Bold = true;
            row++;

            foreach (var t in top5)
            {
                ws.Cell(row, 1).Value = t.Username;
                ws.Cell(row, 2).Value = t.Level;
                ws.Cell(row, 3).Value = t.Xp;
                ws.Cell(row, 4).Value = t.VirtualCurrency;
                row++;
            }

            ws.Columns().AdjustToContents();

            using var ms = new System.IO.MemoryStream();
            wb.SaveAs(ms);
            return ms.ToArray();
        }

        public async Task<byte[]> GenerateActivityOverviewReportAsync(CancellationToken cancellation = default)
        {
            var contents = (await _contentRepo.GetAllAsync()).ToList();
            var exercises = (await _exerciseRepo.GetAllAsync()).ToList();
            var contentProgress = (await _userContentProgressRepo.GetAllAsync()).ToList();
            var exerciseProgress = (await _userExerciseProgressRepo.GetAllAsync()).ToList();

            int totalContents = contents.Count;
            int totalExercises = exercises.Count;

            var contentCompletionCounts = contents.ToDictionary(
                c => c.Id,
                c => contentProgress.Count(p => p.Content.Id == c.Id));

            var contentCompletionRateAvg = contents.Any()
                ? contentCompletionCounts.Values.Average(v => (double)v / Math.Max(1, contentProgress.Select(p => p.GamificationProfile.Id).Distinct().Count()))
                : 0.0;

            var exerciseCompletionCounts = exercises.ToDictionary(
                e => e.Id,
                e => exerciseProgress.Count(p => p.Exercise.Id == e.Id));

            var exerciseCompletionRateAvg = exercises.Any()
                ? exerciseCompletionCounts.Values.Average(v => (double)v / Math.Max(1, exerciseProgress.Select(p => p.GamificationProfile.Id).Distinct().Count()))
                : 0.0;

            var topExercise = exerciseCompletionCounts.OrderByDescending(kv => kv.Value).FirstOrDefault();
            var mostCompletedExercise = topExercise.Key != Guid.Empty ? exercises.FirstOrDefault(e => e.Id == topExercise.Key) : null;
            var mostCompletedCount = topExercise.Value;

            var topContent = contentCompletionCounts.OrderByDescending(kv => kv.Value).FirstOrDefault();
            var mostAccessedContent = topContent.Key != Guid.Empty ? contents.FirstOrDefault(c => c.Id == topContent.Key) : null;
            var mostAccessedCount = topContent.Value;

            var top5Exercises = exerciseCompletionCounts.OrderByDescending(kv => kv.Value)
                .Take(5)
                .Select(kv =>
                {
                    var ex = exercises.FirstOrDefault(e => e.Id == kv.Key);
                    return new
                    {
                        Name = ex?.Question ?? "(unknown)",
                        Completions = kv.Value,
                        Difficulty = ex?.Difficulty ?? "unknown"
                    };
                }).ToList();

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Activity Overview");

            int row = 1;
            ws.Cell(row, 1).Value = "Activity Overview Report";
            ws.Cell(row, 1).Style.Font.Bold = true;
            row += 2;

            ws.Cell(row, 1).Value = "Total Contents";
            ws.Cell(row, 2).Value = totalContents;
            row++;
            ws.Cell(row, 1).Value = "Total Exercises";
            ws.Cell(row, 2).Value = totalExercises;
            row++;

            ws.Cell(row, 1).Value = "Avg Content Completion Rate (per content)";
            ws.Cell(row, 2).Value = contentCompletionRateAvg;
            row++;

            ws.Cell(row, 1).Value = "Avg Exercise Completion Rate (per exercise)";
            ws.Cell(row, 2).Value = exerciseCompletionRateAvg;
            row += 2;

            ws.Cell(row, 1).Value = "Most Completed Exercise";
            ws.Cell(row, 2).Value = mostCompletedExercise?.Question ?? "(none)";
            ws.Cell(row, 3).Value = mostCompletedCount;
            row += 2;

            ws.Cell(row, 1).Value = "Most Accessed Content";
            ws.Cell(row, 2).Value = mostAccessedContent?.Title ?? "(none)";
            ws.Cell(row, 3).Value = mostAccessedCount;
            row += 2;

            ws.Cell(row, 1).Value = "Top 5 Exercises";
            ws.Cell(row, 1).Style.Font.Bold = true;
            row++;
            ws.Cell(row, 1).Value = "Name";
            ws.Cell(row, 2).Value = "Completions";
            ws.Cell(row, 3).Value = "Difficulty";
            ws.Row(row).Style.Font.Bold = true;
            row++;
            foreach (var t in top5Exercises)
            {
                ws.Cell(row, 1).Value = t.Name;
                ws.Cell(row, 2).Value = t.Completions;
                ws.Cell(row, 3).Value = t.Difficulty;
                row++;
            }

            ws.Columns().AdjustToContents();
            using var ms = new System.IO.MemoryStream();
            wb.SaveAs(ms);
            return ms.ToArray();
        }

        public async Task<byte[]> GenerateChallengesSummaryReportAsync(CancellationToken cancellation = default)
        {
            var challenges = (await _challengeRepo.GetAllAsync()).ToList();
            var progresses = (await _userChallengeProgressRepo.GetAllAsync()).ToList();
            var profiles = (await _profileRepo.GetAllAsync()).ToList();

            int totalChallenges = challenges.Count;
            int active = (await _challengeRepo.GetActivesAsync()).Count;
            int inactive = totalChallenges - active;

            var challengeGrouping = challenges.Select(ch =>
            {
                var p = progresses.Where(pr => pr.Challenge.Id == ch.Id).ToList();
                var participants = p.Select(x => x.GamificationProfile.Id).Distinct().Count();
                var completions = p.Count(x => x.IsCompleted);
                double completionRate = participants == 0 ? 0 : (double)completions / participants;
                return new
                {
                    Challenge = ch,
                    Participants = participants,
                    Completions = completions,
                    CompletionRate = completionRate
                };
            }).ToList();

            var averageCompletionOverall = challengeGrouping.Any() ? challengeGrouping.Average(x => x.CompletionRate) : 0.0;
            var averageXpReward = challenges.Any() ? challenges.Average(c => c.ExperienceReward) : 0.0;
            var averageCurrencyReward = challenges.Any() ? challenges.Average(c => c.VirtualCurrencyReward) : 0.0;

            var top5ByParticipants = challengeGrouping.OrderByDescending(x => x.Participants)
                .Take(5)
                .Select(x => new
                {
                    Name = x.Challenge.Name,
                    Participants = x.Participants,
                    CompletionRate = x.CompletionRate,
                    XpReward = x.Challenge.ExperienceReward,
                    CurrencyReward = x.Challenge.VirtualCurrencyReward
                }).ToList();

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Challenges Summary");

            int row = 1;
            ws.Cell(row, 1).Value = "Challenges Summary Report";
            ws.Cell(row, 1).Style.Font.Bold = true;
            row += 2;

            ws.Cell(row, 1).Value = "Total Challenges";
            ws.Cell(row, 2).Value = totalChallenges;
            row++;

            ws.Cell(row, 1).Value = "Active Challenges";
            ws.Cell(row, 2).Value = active;
            row++;

            ws.Cell(row, 1).Value = "Inactive Challenges";
            ws.Cell(row, 2).Value = inactive;
            row++;

            ws.Cell(row, 1).Value = "Average Completion Rate (per challenge)";
            ws.Cell(row, 2).Value = averageCompletionOverall;
            row++;

            ws.Cell(row, 1).Value = "Average XP Reward";
            ws.Cell(row, 2).Value = averageXpReward;
            row++;

            ws.Cell(row, 1).Value = "Average Virtual Currency Reward";
            ws.Cell(row, 2).Value = averageCurrencyReward;
            row += 2;

            ws.Cell(row, 1).Value = "Top 5 Challenges by Participants";
            ws.Cell(row, 1).Style.Font.Bold = true;
            row++;
            ws.Cell(row, 1).Value = "Name";
            ws.Cell(row, 2).Value = "Participants";
            ws.Cell(row, 3).Value = "Avg Completion %";
            ws.Cell(row, 4).Value = "XP Reward";
            ws.Cell(row, 5).Value = "Currency Reward";
            ws.Row(row).Style.Font.Bold = true;
            row++;
            foreach (var t in top5ByParticipants)
            {
                ws.Cell(row, 1).Value = t.Name;
                ws.Cell(row, 2).Value = t.Participants;
                ws.Cell(row, 3).Value = t.CompletionRate;
                ws.Cell(row, 4).Value = t.XpReward;
                ws.Cell(row, 5).Value = t.CurrencyReward;
                row++;
            }

            ws.Columns().AdjustToContents();
            using var ms = new MemoryStream();
            wb.SaveAs(ms);
            return ms.ToArray();
        }
    }
}
