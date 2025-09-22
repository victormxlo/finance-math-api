using FinanceMath.Application.Gamification.Achievements.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Commands
{
    public class CreateAchievementCommand : IRequest<Result<AchievementDto>>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string CriteriaKey { get; set; }
        public int ExperienceReward { get; set; }
        public int VirtualCurrencyReward { get; set; }
    }
}
