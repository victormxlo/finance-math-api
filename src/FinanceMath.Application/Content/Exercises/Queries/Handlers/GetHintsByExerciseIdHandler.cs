using AutoMapper;
using FinanceMath.Application.Content.Exercises.Dtos;
using FinanceMath.Application.Content.Exercises.Queries;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Exercises.Queries.Handlers
{
    public class GetHintsByExerciseIdHandler : IRequestHandler<GetHintsByExerciseIdQuery, Result<ICollection<ExerciseHintDto>>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public GetHintsByExerciseIdHandler(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<ExerciseHintDto>>> Handle(GetHintsByExerciseIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var exercise = await _exerciseRepository.GetByIdAsync(request.Id);

                if (exercise == null)
                    return Result<ICollection<ExerciseHintDto>>.Fail($"Exercise not found with id: {request.Id}.");

                if (exercise.Hints == null || exercise.Hints?.Count == 0)
                    return Result<ICollection<ExerciseHintDto>>.Fail($"Exercise {exercise.Id} has no hints.");

                var hintsDto = _mapper.Map<ICollection<ExerciseHintDto>>(exercise.Hints);

                return Result<ICollection<ExerciseHintDto>>.Ok(hintsDto);
            }
            catch (Exception ex)
            {
                return Result<ICollection<ExerciseHintDto>>.Fail($"Failed to get hints by exercise id: {ex.Message}.");
            }
        }
    }
}
