using AutoMapper;
using FinanceMath.Application.Content.Exercises.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Exercises.Queries.Handlers
{
    public class GetUserExerciseProgressHandler : IRequestHandler<GetUserExerciseProgressQuery, Result<ICollection<UserExerciseProgressDto>>>
    {
        private readonly IUserExerciseProgressRepository _userExerciseProgressRepository;
        private readonly IMapper _mapper;

        public GetUserExerciseProgressHandler(IUserExerciseProgressRepository userExerciseProgressRepository, IMapper mapper)
        {
            _userExerciseProgressRepository = userExerciseProgressRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<UserExerciseProgressDto>>> Handle(GetUserExerciseProgressQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var progresses = await _userExerciseProgressRepository.GetByUserIdAsync(request.UserId);

                if (progresses is null)
                    return Result<ICollection<UserExerciseProgressDto>>.Fail($"No exercise progress found with user id: {request.UserId}.");

                var dtos = _mapper.Map<ICollection<UserExerciseProgressDto>>(progresses);

                return Result<ICollection<UserExerciseProgressDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return Result<ICollection<UserExerciseProgressDto>>.Fail($"Failed to get user exercise progress: {ex.Message}.");
            }
        }
    }
}
