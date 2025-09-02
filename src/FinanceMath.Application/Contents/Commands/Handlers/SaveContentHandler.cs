using AutoMapper;
using FinanceMath.Application.Contents.Dtos;
using FinanceMath.Domain.ContentAggregate;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Contents.Commands.Handlers
{
    public class SaveContentHandler : IRequestHandler<SaveContentCommand, Result<ContentDto>>
    {
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;

        public SaveContentHandler(IContentRepository repository, IMapper mapper)
        {
            _contentRepository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ContentDto>> Handle(SaveContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var content = new Content(
                    title: request.Title, body: request.Body, categoryId: request.CategoryId,
                    createdBy: request.CreatedBy, mediaUrl: request.MediaUrl);

                await _contentRepository.SaveAsync(content);

                var dto = _mapper.Map<ContentDto>(content);

                return Result<ContentDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<ContentDto>.Fail($"Failed to save content: {ex.Message}");
            }
        }
    }
}
