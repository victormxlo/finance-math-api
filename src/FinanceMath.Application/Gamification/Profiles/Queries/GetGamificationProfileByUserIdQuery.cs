using FinanceMath.Application.Gamification.Profiles.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Profiles.Queries
{
    public class GetGamificationProfileByUserIdQuery : IRequest<Result<GamificationProfileDto>>
    {
        public Guid UserId { get; set; }
    }
}
