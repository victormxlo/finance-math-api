using AutoMapper;
using FinanceMath.Application.Contents.Dtos;
using FinanceMath.Domain.ContentAggregate;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Contents.Commands.Handlers
{
    public class CreateContentHandler : IRequestHandler<CreateContentCommand, Result<ContentDto>>
    {
        private readonly IContentRepository _contentRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateContentHandler(IContentRepository repository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _contentRepository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<ContentDto>> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

                if (category == null)
                    return Result<ContentDto>.Fail($"No category found with id: {request.CategoryId}.");

                var content = new Content(
                    title: request.Title, body: request.Body, category: category,
                    createdBy: request.CreatedBy, mediaUrl: request.MediaUrl);

                await _contentRepository.SaveAsync(content);

                var dto = _mapper.Map<ContentDto>(content);

                return Result<ContentDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<ContentDto>.Fail($"Failed to create content: {ex.Message}.");
            }
        }
    }
}
