using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Commands
{
    public class DeleteChallengeCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}
