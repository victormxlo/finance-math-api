using AutoMapper;
using FinanceMath.Application.Content.ContentSections.Dtos;
using FinanceMath.Application.Content.ContentSections.Queries;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.ContentSections.Queries.Handlers
{
    public class GetAllContentSectionsByContentIdHandler : IRequestHandler<GetAllContentSectionsByContentIdQuery, Result<ICollection<ContentSectionDto>>>
    {
        private readonly IContentSectionRepository _contentSectionRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;

        public GetAllContentSectionsByContentIdHandler(
            IContentSectionRepository contentSectionRepository,
            IContentRepository contentRepository,
            IMapper mapper)
        {
            _contentSectionRepository = contentSectionRepository;
            _contentRepository = contentRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<ContentSectionDto>>> Handle(GetAllContentSectionsByContentIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var content = await _contentRepository.GetByIdAsync(request.ContentId);

                if (content == null)
                    return Result<ICollection<ContentSectionDto>>.Fail($"Content not found with id: {request.ContentId}.");

                var contentSections = await _contentSectionRepository.GetAllByContentIdAsync(content.Id);
                var contentSectionsDto = _mapper.Map<ICollection<ContentSectionDto>>(contentSections);

                return Result<ICollection<ContentSectionDto>>.Ok(contentSectionsDto);
            }
            catch (Exception ex)
            {
                return Result<ICollection<ContentSectionDto>>.Fail($"Failed to get content sections by content id: {ex.Message}.");
            }
        }
    }
}
