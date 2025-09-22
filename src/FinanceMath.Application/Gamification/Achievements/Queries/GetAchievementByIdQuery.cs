using FinanceMath.Application.Gamification.Achievements.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Queries
{
    public class GetAchievementByIdQuery : IRequest<Result<AchievementDto>>
    {
        public Guid Id { get; set; }
    }
}
