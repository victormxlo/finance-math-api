using AutoMapper;
using FinanceMath.Application.Content.Exercises.Commands;
using FinanceMath.Application.Content.Exercises.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Exercises.Commands.Handlers
{
    public class UpdateExerciseHandler : IRequestHandler<UpdateExerciseCommand, Result<ExerciseDto>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public UpdateExerciseHandler(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public async Task<Result<ExerciseDto>> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exercise = await _exerciseRepository.GetByIdAsync(request.Id);

                if (exercise == null)
                    return Result<ExerciseDto>.Fail($"Exercise not found with id: {request.Id}.");

                exercise.Update(request.Question, request.Explanation, request.Difficulty);

                foreach (var optDto in request.Options)
                {
                    if (optDto.Remove && optDto.Id.HasValue)
                    {
                        exercise.RemoveOption(optDto.Id.Value);
                        continue;
                    }

                    if (!optDto.Id.HasValue)
                    {
                        exercise.AddOption(optDto.Description, optDto.IsCorrect, optDto.Order);
                    }
                    else
                    {
                        var opt = exercise.Options.FirstOrDefault(o => o.Id == optDto.Id);
                        if (opt != null)
                        {
                            opt.Update(optDto.Description, optDto.Order, optDto.IsCorrect);
                            if (optDto.IsCorrect)
                                exercise.SetCorrectOption(opt.Id);
                        }
                    }
                }

                foreach (var hintDto in request.Hints)
                {
                    if (hintDto.Remove && hintDto.Id.HasValue)
                    {
                        exercise.RemoveHint(hintDto.Id.Value);
                        continue;
                    }

                    if (!hintDto.Id.HasValue)
                    {
                        exercise.AddHint(hintDto.Description, hintDto.Order);
                    }
                    else
                    {
                        var hint = exercise.Hints.FirstOrDefault(h => h.Id == hintDto.Id);
                        hint?.Update(hintDto.Description, hintDto.Order);
                    }
                }

                exercise.Validate();

                await _exerciseRepository.UpdateAsync(exercise);

                var dto = _mapper.Map<ExerciseDto>(exercise);

                return Result<ExerciseDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<ExerciseDto>.Fail($"Failed to update exercise: {ex.Message}.");
            }
        }
    }
}
