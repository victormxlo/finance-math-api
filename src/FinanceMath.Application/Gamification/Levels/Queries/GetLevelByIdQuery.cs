using FinanceMath.Application.Gamification.Levels.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Levels.Queries
{
    public class GetLevelByIdQuery : IRequest<Result<LevelDto>>
    {
        public int Id { get; set; }
    }
}
