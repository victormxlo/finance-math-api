using FinanceMath.Application.Content.ContentSections.Dtos;
using MediatR;

namespace FinanceMath.Application.Content.ContentSections.Commands
{
    public class CreateContentSectionCommand : IRequest<Result<ContentSectionDto>>
    {
        public Guid ContentId { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
        public int Order { get; set; }
    }
}
