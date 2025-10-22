using FinanceMath.Application.Content.Exercises.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.Exercises.Queries
{
    public class GetUserExerciseProgressQuery : IRequest<Result<ICollection<UserExerciseProgressDto>>>
    {
        public Guid UserId { get; set; }
    }
}
