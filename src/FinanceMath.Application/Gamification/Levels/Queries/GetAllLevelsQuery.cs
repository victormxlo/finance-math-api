using FinanceMath.Application.Gamification.Levels.Dtos;
using MediatR;

namespace FinanceMath.Application.Gamification.Levels.Queries
{
    public class GetAllLevelsQuery : IRequest<Result<ICollection<LevelDto>>> { }
}
