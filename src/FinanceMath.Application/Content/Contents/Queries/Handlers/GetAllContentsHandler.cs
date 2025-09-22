using AutoMapper;
using FinanceMath.Application.Content.Contents.Dtos;
using FinanceMath.Domain.Repositories;
using MediatR;

namespace FinanceMath.Application.Content.Contents.Queries.Handlers
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
            try
            {
                var contents = await _contentRepository.GetAllAsync();

                if (contents == null || contents?.Count == 0)
                    return Result<ICollection<ContentDto>>.Fail("No content found.");

                var dtos = _mapper.Map<ICollection<ContentDto>>(contents);

                return Result<ICollection<ContentDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return Result<ICollection<ContentDto>>.Fail($"Failed to get all contents {ex.Message}.");
            }
        }
    }
}
