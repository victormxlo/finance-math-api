using FinanceMath.Application.Content.Exercises.Commands;
using FinanceMath.Application.Content.Exercises.Dtos;
using FinanceMath.Application.Interfaces;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Exercise.Exercises.Commands.Handlers
{
    internal class CompleteExerciseHandler : IRequestHandler<CompleteExerciseCommand, Result<CompleteExerciseResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IGamificationService _gamificationService;

        public CompleteExerciseHandler(IUserRepository userRepository, IExerciseRepository exerciseRepository, IGamificationService gamificationService)
        {
            _userRepository = userRepository;
            _exerciseRepository = exerciseRepository;
            _gamificationService = gamificationService;
        }

        public async Task<Result<CompleteExerciseResponseDto>> Handle(CompleteExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);

                if (user is null)
                    return Result<CompleteExerciseResponseDto>.Fail($"User not found with id: {request.UserId}.");

                var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId);

                if (exercise is null)
                    return Result<CompleteExerciseResponseDto>.Fail($"Exercise not found with id: {request.ExerciseId}.");

                var option = exercise.Options.First(opt => opt.Id == request.ExerciseOptionId);

                if (option is null)
                    return Result<CompleteExerciseResponseDto>.Fail($"Option not found with {request.ExerciseOptionId}.");

                var response = await _gamificationService.CompleteExerciseAsync(
                    user.Id, exercise.Id, request.ExerciseOptionId,
                    request.UsedHint ?? false);

                return Result<CompleteExerciseResponseDto>.Ok(response);
            }
            catch (Exception ex)
            {
                return Result<CompleteExerciseResponseDto>.Fail($"Failed to complete exercise: {ex.Message}.");
            }
        }
    }
}
