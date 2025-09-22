using FinanceMath.Application.Gamification.Challenges.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Queries
{
    public class GetChallengeByIdQuery : IRequest<Result<ChallengeDto>>
    {
        public Guid Id { get; set; }
    }
}
