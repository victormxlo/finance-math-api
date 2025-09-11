using AutoMapper;
using FinanceMath.Application.Exercises.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Exercises.Queries.Handlers
{
    public class GetAllExercisesHandler : IRequestHandler<GetAllExercisesQuery, Result<ICollection<ExerciseDto>>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public GetAllExercisesHandler(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<ExerciseDto>>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var exercises = await _exerciseRepository.GetAllAsync();

                if (exercises == null || exercises.Count == 0)
                    return Result<ICollection<ExerciseDto>>.Fail("No exercises found.");

                var dtos = _mapper.Map<ICollection<ExerciseDto>>(exercises);

                return Result<ICollection<ExerciseDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return Result<ICollection<ExerciseDto>>.Fail($"Failed to get all exercises: {ex.Message}.");
            }
        }
    }
}
