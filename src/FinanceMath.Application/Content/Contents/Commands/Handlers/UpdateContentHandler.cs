using AutoMapper;
using FinanceMath.Application.Content.Contents.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Contents.Commands.Handlers
{
    public class UpdateContentHandler : IRequestHandler<UpdateContentCommand, Result<ContentDto>>
    {
        private readonly IContentRepository _contentRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateContentHandler(IContentRepository repository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _contentRepository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<ContentDto>> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var content = await _contentRepository.GetByIdAsync(request.Id);

                if (content == null)
                    return Result<ContentDto>.Fail($"No content found with id: {request.Id}.");

                var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

                if (category == null)
                    return Result<ContentDto>.Fail($"No category found with id: {request.CategoryId}.");

                content.Update(request.Title, request.Body, category, request.MediaUrl);

                await _contentRepository.UpdateAsync(content);

                var dto = _mapper.Map<ContentDto>(content);

                return Result<ContentDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<ContentDto>.Fail($"Failed to update content: {ex.Message}");
            }
        }
    }
}
