using FinanceMath.Application.Gamification.Achievements.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Queries
{
    public class GetUserAchievementProgressQuery : IRequest<Result<ICollection<UserAchievementProgressDto>>>
    {
        public Guid UserId { get; set; }
    }
}
