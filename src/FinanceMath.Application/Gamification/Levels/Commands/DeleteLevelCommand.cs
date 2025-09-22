using MediatR;

namespace FinanceMath.Application.Gamification.Levels.Commands
{
    public class DeleteLevelCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
    }
}
