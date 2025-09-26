using FinanceMath.Application.Gamification.Achievements.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Queries
{
    public class GetAchievementsByUserIdQuery : IRequest<Result<ICollection<AchievementDto>>>
    {
        public Guid UserId { get; set; }
    }
}
