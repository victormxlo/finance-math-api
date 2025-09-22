using AutoMapper;
using FinanceMath.Application.Content.Contents.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Contents.Queries.Handlers
{
    public class GetContentByIdHandler : IRequestHandler<GetContentByIdQuery, Result<ContentDto>>
    {
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;

        public GetContentByIdHandler(IContentRepository contentRepository, IMapper mapper)
        {
            _contentRepository = contentRepository;
            _mapper = mapper;
        }

        public async Task<Result<ContentDto>> Handle(GetContentByIdQuery request, CancellationToken cancellationToken)
        {
            var content = await _contentRepository.GetByIdAsync(request.Id);

            if (content == null)
                return Result<ContentDto>.Fail($"No content found with id: {request.Id}.");

            var dto = _mapper.Map<ContentDto>(content);

            return Result<ContentDto>.Ok(dto);
        }
    }
}
