using FinanceMath.Application.Gamification.Leaderboards.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Leaderboards.Queries
{
    public class GetLeaderboardQuery : IRequest<Result<ICollection<LeaderboardEntryDto>>>
    {
        public int? Top { get; set; }
    }
}
