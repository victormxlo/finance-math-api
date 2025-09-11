using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Exercises.Commands.Handlers
{
    public class DeleteExerciseHandler : IRequestHandler<DeleteExerciseCommand, Result<Unit>>
    {
        private readonly IExerciseRepository _exerciseRepository;

        public DeleteExerciseHandler(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public async Task<Result<Unit>> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exercise = await _exerciseRepository.GetByIdAsync(request.Id);

                if (exercise == null)
                    return Result<Unit>.Fail($"Exercise not found with id: {request.Id}.");

                await _exerciseRepository.DeleteAsync(exercise);

                return Result<Unit>.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit>.Fail($"An error occurred: {ex.Message}.");
            }
        }
    }
}
