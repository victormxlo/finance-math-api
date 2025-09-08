using FinanceMath.Application.ContentSections.Dtos;
using MediatR;

namespace FinanceMath.Application.ContentSections.Commands
{
    public class CreateContentSectionCommand : IRequest<Result<ContentSectionDto>>
    {
        public Guid ContentId { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
        public int Order { get; set; }
    }
}
