using AutoMapper;
using FinanceMath.Application.Contents.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Contents.Commands.Handlers
{
    public class UpdateContentHandler : IRequestHandler<UpdateContentCommand, Result<ContentDto>>
    {
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;

        public UpdateContentHandler(IContentRepository repository, IMapper mapper)
        {
            _contentRepository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ContentDto>> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var content = await _contentRepository.GetByIdAsync(request.Id);

                if (content == null)
                    return Result<ContentDto>.Fail($"No content found with id: {request.Id}.");

                content.Update(request.Title, request.Body, request.CategoryId, request.MediaUrl);

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
