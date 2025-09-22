using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Contents.Commands.Handlers
{
    public class DeleteContentHandler : IRequestHandler<DeleteContentCommand, Result<bool>>
    {
        private readonly IContentRepository _contentRepository;

        public DeleteContentHandler(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public async Task<Result<bool>> Handle(DeleteContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var content = await _contentRepository.GetByIdAsync(request.Id);

                if (content == null)
                    return Result<bool>.Fail("Content not found.");

                await _contentRepository.DeleteAsync(content);

                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Failed to delete content: {ex.Message}.");
            }
        }
    }
}
