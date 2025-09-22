using AutoMapper;
using FinanceMath.Application.Content.ContentSections.Commands;
using FinanceMath.Application.Content.ContentSections.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.ContentSections.Commands.Handlers
{
    public class UpdateContentSectionHandler : IRequestHandler<UpdateContentSectionCommand, Result<ContentSectionDto>>
    {
        private readonly IContentSectionRepository _contentSectionRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;

        public UpdateContentSectionHandler(
            IContentSectionRepository contentSectionRepository,
            IContentRepository contentRepository,
            IMapper mapper)
        {
            _contentSectionRepository = contentSectionRepository;
            _contentRepository = contentRepository;
            _mapper = mapper;
        }

        public async Task<Result<ContentSectionDto>> Handle(UpdateContentSectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var content = await _contentRepository.GetByIdAsync(request.ContentId);

                if (content == null)
                    return Result<ContentSectionDto>.Fail($"Content not found with id: {request.ContentId}.");

                var contentSection = await _contentSectionRepository.GetByIdAsync(request.ContentSectionId);

                if (contentSection == null)
                    return Result<ContentSectionDto>
                        .Fail($"Content section not found with id: {request.ContentSectionId}.");

                contentSection.Update(request.Title, request.Body, request.Order);

                await _contentSectionRepository.UpdateAsync(contentSection);

                var contentSectionDto = _mapper.Map<ContentSectionDto>(contentSection);

                return Result<ContentSectionDto>.Ok(contentSectionDto);
            }
            catch (Exception ex)
            {
                return Result<ContentSectionDto>.Fail($"Failed to update content section: {ex.Message}.");
            }
        }
    }
}
