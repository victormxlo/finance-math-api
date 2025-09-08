using FinanceMath.Application.ContentSections.Dtos;
using MediatR;

namespace FinanceMath.Application.ContentSections.Queries
{
    public class GetAllContentSectionsByContentIdQuery : IRequest<Result<ICollection<ContentSectionDto>>>
    {
        public Guid ContentId { get; set; }
    }
}
