using AutoMapper;
using FinanceMath.Application.Content.ContentSections.Commands;
using FinanceMath.Application.Content.ContentSections.Dtos;
using FinanceMath.Domain.ContentAggregate;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.ContentSections.Commands.Handlers
{
    public class CreateContentSectionHandler : IRequestHandler<CreateContentSectionCommand, Result<ContentSectionDto>>
    {
        private readonly IContentSectionRepository _contentSectionRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;

        public CreateContentSectionHandler(
            IContentSectionRepository contentSectionRepository,
            IContentRepository contentRepository,
            IMapper mapper)
        {
            _contentSectionRepository = contentSectionRepository;
            _contentRepository = contentRepository;
            _mapper = mapper;
        }

        public async Task<Result<ContentSectionDto>> Handle(CreateContentSectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var content = await _contentRepository.GetByIdAsync(request.ContentId);

                if (content == null)
                    return Result<ContentSectionDto>.Fail($"Content not found with id: {request.ContentId}.");

                var contentSection = new ContentSection(request.Title, request.Body, request.Order, content);

                await _contentSectionRepository.SaveAsync(contentSection);

                var contentSectionDto = _mapper.Map<ContentSectionDto>(contentSection);

                return Result<ContentSectionDto>.Ok(contentSectionDto);
            }
            catch (Exception ex)
            {
                return Result<ContentSectionDto>.Fail($"Failed to save content section: {ex.Message}.");
            }
        }
    }
}
