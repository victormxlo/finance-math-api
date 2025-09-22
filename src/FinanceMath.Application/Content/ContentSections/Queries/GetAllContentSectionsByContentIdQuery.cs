using FinanceMath.Application.Content.ContentSections.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.ContentSections.Queries
{
    public class GetAllContentSectionsByContentIdQuery : IRequest<Result<ICollection<ContentSectionDto>>>
    {
        public Guid ContentId { get; set; }
    }
}
