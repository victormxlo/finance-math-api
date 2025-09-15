using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Exercises.Queries.Handlers
{
    public class GetExplanationByExerciseIdHandler : IRequestHandler<GetExplanationByExerciseIdQuery, Result<string>>
    {
        private readonly IExerciseRepository _exerciseRepository;

        public GetExplanationByExerciseIdHandler(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public async Task<Result<string>> Handle(GetExplanationByExerciseIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var exercise = await _exerciseRepository.GetByIdAsync(request.Id);

                if (exercise == null)
                    return Result<string>.Fail($"Exercise not found with id: {request.Id}.");

                var explanation = exercise.Explanation;

                if (string.IsNullOrEmpty(explanation))
                    return Result<string>.Fail($"Exercise {request.Id} has no explanation.");

                return Result<string>.Ok(explanation);
            }
            catch (Exception ex)
            {
                return Result<string>.Fail($"Failed to get exercise explanation: {ex.Message}.");
            }
        }
    }
}
