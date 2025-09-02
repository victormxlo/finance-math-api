using AutoMapper;
using FinanceMath.Application.Contents.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Contents.Queries.Handlers
{
    public class GetAllContentsHandler : IRequestHandler<GetAllContentsQuery, Result<ICollection<ContentDto>>>
    {
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;

        public GetAllContentsHandler(IContentRepository repository, IMapper mapper)
        {
            _contentRepository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<ContentDto>>> Handle(GetAllContentsQuery request, CancellationToken cancellationToken)
        {
            var contents = await _contentRepository.GetAllAsync();

            if (contents == null || contents?.Count == 0)
                return Result<ICollection<ContentDto>>.Fail("No content found.");

            var dtos = _mapper.Map<ICollection<ContentDto>>(contents);

            return Result<ICollection<ContentDto>>.Ok(dtos);
        }
    }
}
