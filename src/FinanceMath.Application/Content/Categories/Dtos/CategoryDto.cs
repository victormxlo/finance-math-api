namespace FinanceMath.Application.Content.Categories.Dtos
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
        public ICollection<Guid> SubcategoryIds { get; set; } = new List<Guid>();
        public ICollection<Guid> ContentIds { get; set; } = new List<Guid>();
    }
}
