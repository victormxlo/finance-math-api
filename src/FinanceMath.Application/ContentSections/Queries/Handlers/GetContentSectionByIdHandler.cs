using AutoMapper;
using FinanceMath.Application.ContentSections.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.ContentSections.Queries.Handlers
{
    public class GetContentSectionByIdHandler : IRequestHandler<GetContentSectionByIdQuery, Result<ContentSectionDto>>
    {
        private readonly IContentSectionRepository _contentSectionRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;

        public GetContentSectionByIdHandler(
            IContentSectionRepository contentSectionRepository,
            IContentRepository contentRepository,
            IMapper mapper)
        {
            _contentSectionRepository = contentSectionRepository;
            _contentRepository = contentRepository;
            _mapper = mapper;
        }

        public async Task<Result<ContentSectionDto>> Handle(GetContentSectionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var content = await _contentRepository.GetByIdAsync(request.ContentId);

                if (content == null)
                    return Result<ContentSectionDto>.Fail($"Content not found with id: {request.ContentId}.");

                var contentSection = await _contentSectionRepository.GetByIdAsync(request.ContentSectionId);

                if (contentSection == null)
                    return Result<ContentSectionDto>.Fail($"Content section not found with id: {request.ContentSectionId}.");

                var contentSectionDto = _mapper.Map<ContentSectionDto>(contentSection);

                return Result<ContentSectionDto>.Ok(contentSectionDto);
            }
            catch (Exception ex)
            {
                return Result<ContentSectionDto>.Fail($"Failed to get content section by id: {ex.Message}.");
            }
        }
    }
}
