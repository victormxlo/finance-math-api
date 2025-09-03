using MediatR;

namespace FinanceMath.Application.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}
