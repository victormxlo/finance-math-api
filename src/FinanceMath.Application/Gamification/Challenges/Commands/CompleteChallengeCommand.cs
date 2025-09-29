using FinanceMath.Application.Gamification.Challenges.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Commands
{
    public class CompleteChallengeCommand : IRequest<Result<CompleteChallengeResponseDto>>
    {
        public Guid UserId { get; set; }
        public Guid ChallengeId { get; set; }
    }
}
