using AutoMapper;
using FinanceMath.Application.Content.ContentSections.Commands;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.ContentSections.Commands.Handlers
{
    internal class DeleteContentSectionHandler : IRequestHandler<DeleteContentSectionCommand, Result<bool>>
    {
        private readonly IContentSectionRepository _contentSectionRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;

        public DeleteContentSectionHandler(
            IContentSectionRepository contentSectionRepository,
            IContentRepository contentRepository,
            IMapper mapper)
        {
            _contentSectionRepository = contentSectionRepository;
            _contentRepository = contentRepository;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(DeleteContentSectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var content = await _contentRepository.GetByIdAsync(request.ContentId);

                if (content == null)
                    return Result<bool>.Fail($"Content not found with id: {request.ContentId}.");

                var contentSection = await _contentSectionRepository.GetByIdAsync(request.ContentSectionId);

                if (contentSection == null)
                    return Result<bool>
                        .Fail($"Content section not found with id: {request.ContentSectionId}.");

                await _contentSectionRepository.DeleteAsync(contentSection);

                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Failed to delete content section: {ex.Message}.");
            }
        }
    }
}
