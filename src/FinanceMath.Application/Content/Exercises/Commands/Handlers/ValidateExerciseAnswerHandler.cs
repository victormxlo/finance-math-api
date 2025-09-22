using FinanceMath.Application.Content.Exercises.Commands;
using FinanceMath.Application.Content.Exercises.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Exercises.Commands.Handlers
{
    public class ValidateExerciseAnswerHandler : IRequestHandler<ValidateExerciseAnswerCommand, Result<ValidateExerciseAnswerDto>>
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ValidateExerciseAnswerHandler(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public async Task<Result<ValidateExerciseAnswerDto>> Handle(ValidateExerciseAnswerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId);

                if (exercise == null)
                    return Result<ValidateExerciseAnswerDto>.Fail($"Exercise not found with {request.ExerciseId}.");

                var option = exercise.Options.First(opt => opt.Id == request.ExerciseOptionId);

                if (option == null)
                    return Result<ValidateExerciseAnswerDto>.Fail($"Option not found with {request.ExerciseOptionId}.");

                ValidateExerciseAnswerDto dto = new ValidateExerciseAnswerDto
                {
                    ExerciseId = exercise.Id,
                    ExerciseOptionId = option.Id,
                    IsCorrect = option.IsCorrect
                };

                return Result<ValidateExerciseAnswerDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<ValidateExerciseAnswerDto>.Fail($"Failed to get the exercise answer: {ex.Message}.");
            }
        }
    }
}
