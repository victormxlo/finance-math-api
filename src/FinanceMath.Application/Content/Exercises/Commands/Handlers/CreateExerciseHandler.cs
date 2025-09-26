using AutoMapper;
using FinanceMath.Application.Content.Exercises.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Exercises.Commands.Handlers
{
    public class CreateExerciseHandler : IRequestHandler<CreateExerciseCommand, Result<ExerciseDto>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public CreateExerciseHandler(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public async Task<Result<ExerciseDto>> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exercise = new Domain.ContentAggregate.Exercise(
                    request.Question,
                    request.Explanation,
                    request.Difficulty
                );

                foreach (var option in request.Options)
                    exercise.AddOption(option.Description, option.IsCorrect, option.Order);

                foreach (var hint in request.Hints)
                    exercise.AddHint(hint.Description, hint.Order);

                exercise.Validate();

                await _exerciseRepository.SaveAsync(exercise);

                var dto = _mapper.Map<ExerciseDto>(exercise);

                return Result<ExerciseDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<ExerciseDto>.Fail($"Failed to create exercise: {ex.Message}.");
            }
        }
    }
}