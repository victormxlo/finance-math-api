using FinanceMath.Application.Gamification.Achievements.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Commands
{
    public class UnlockUserAchievementCommand : IRequest<Result<AchievementDto>>
    {
        public Guid UserId { get; set; }
        public Guid AchievementId { get; set; }
        public required string CriteriaKey { get; set; }
    }
}
