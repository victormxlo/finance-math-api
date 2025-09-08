using FinanceMath.Application.ContentSections.Dtos;
using MediatR;

namespace FinanceMath.Application.ContentSections.Queries
{
    public class GetContentSectionByIdQuery : IRequest<Result<ContentSectionDto>>
    {
        public Guid ContentId { get; set; }
        public Guid ContentSectionId { get; set; }
    }
}
