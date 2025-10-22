using FinanceMath.Application.Gamification.Challenges.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Challenges.Queries
{
    public class GetUserChallengeProgressQuery : IRequest<Result<ICollection<UserChallengeProgressDto>>>
    {
        public Guid UserId { get; set; }
    }
}
