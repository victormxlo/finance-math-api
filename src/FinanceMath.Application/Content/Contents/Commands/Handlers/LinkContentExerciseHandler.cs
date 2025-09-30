using FinanceMath.Application.Content.Contents.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Contents.Commands.Handlers
{
    public class LinkContentExerciseHandler : IRequestHandler<LinkContentExerciseCommand, Result<LinkContentExerciseDto>>
    {
        private readonly IContentRepository _contentRepository;
        private readonly IExerciseRepository _exerciseRepository;

        public LinkContentExerciseHandler(IContentRepository contentRepository, IExerciseRepository exerciseRepository)
        {
            _contentRepository = contentRepository;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<Result<LinkContentExerciseDto>> Handle(LinkContentExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var content = await _contentRepository.GetByIdAsync(request.ContentId);

                if (content is null)
                    return Result<LinkContentExerciseDto>.Fail($"Content not found with id: {request.ContentId}.");

                var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId);

                if (exercise is null)
                    return Result<LinkContentExerciseDto>.Fail($"Exercise not found with id: {request.ExerciseId}.");

                content.LinkExercise(exercise);
                await _contentRepository.UpdateAsync(content);

                if (!content.HasLinkedExercise(exercise))
                    return Result<LinkContentExerciseDto>.Fail("Unable to link-up.");

                var dto = new LinkContentExerciseDto { ContentId = content.Id, ExerciseId = exercise.Id };

                return Result<LinkContentExerciseDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<LinkContentExerciseDto>.Fail($"Failed to link exercise in content: {ex.Message}.");
            }
        }
    }
}
