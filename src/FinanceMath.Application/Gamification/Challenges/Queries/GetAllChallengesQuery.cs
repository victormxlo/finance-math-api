using FinanceMath.Application.Gamification.Challenges.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Queries
{
    public class GetAllChallengesQuery : IRequest<Result<ChallengeDto>>
    {
        public bool Active { get; set; } = false;
    }
}
