using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Commands
{
    public class CompleteChallengeCommand : IRequest<Result<Unit>>
    {
        public Guid UserId { get; set; }
        public Guid ChallengeId { get; set; }
    }
}
