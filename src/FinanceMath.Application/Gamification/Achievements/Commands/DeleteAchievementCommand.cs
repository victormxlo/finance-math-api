using MediatR;

namespace FinanceMath.Application.Gamification.Achievements.Commands
{
    public class DeleteAchievementCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}
