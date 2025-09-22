using FinanceMath.Application.Gamification.Achievements.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Queries
{
    public class GetAllAchievementsQuery : IRequest<Result<ICollection<AchievementDto>>> { }
}
