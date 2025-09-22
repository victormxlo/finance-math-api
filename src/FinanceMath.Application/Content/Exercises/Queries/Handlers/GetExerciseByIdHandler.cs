using AutoMapper;
using FinanceMath.Application.Content.Exercises.Dtos;
using FinanceMath.Application.Content.Exercises.Queries;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Exercises.Queries.Handlers
{
    public class GetExerciseByIdHandler : IRequestHandler<GetExerciseByIdQuery, Result<ExerciseDto>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public GetExerciseByIdHandler(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public async Task<Result<ExerciseDto>> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var exercise = await _exerciseRepository.GetByIdAsync(request.Id);

                if (exercise == null)
                    return Result<ExerciseDto>.Fail($"Exercise not found with id: {request.Id}.");

                var dto = _mapper.Map<ExerciseDto>(exercise);

                return Result<ExerciseDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<ExerciseDto>.Fail($"Failed to get exercise by id: {ex.Message}.");
            }
        }
    }
}