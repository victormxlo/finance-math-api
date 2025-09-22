using FinanceMath.Application.Content.ContentSections.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.ContentSections.Queries
{
    public class GetContentSectionByIdQuery : IRequest<Result<ContentSectionDto>>
    {
        public Guid ContentId { get; set; }
        public Guid ContentSectionId { get; set; }
    }
}
