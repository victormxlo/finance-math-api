namespace FinanceMath.Domain.ContentAggregate
{
    public class Category
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual Category? ParentCategory { get; protected set; }
        public virtual ICollection<Category> Subcategories { get; protected set; } = new List<Category>();
        public virtual ICollection<Content> Contents { get; protected set; } = new List<Content>();

        protected Category() { }

        public Category(string name, Category? parentCategory = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            ParentCategory = parentCategory;

            parentCategory?.AddSubcategory(this);
        }

        public virtual void AddSubcategory(Category subCategory)
        {
            if (subCategory == null) throw new ArgumentNullException(nameof(subCategory));

            Subcategories.Add(subCategory);
        }

        public virtual void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name cannot be empty.");

            Name = name;
        }

        public virtual void SetParentCategory(Category parentCategory)
        {
            ParentCategory = parentCategory;
            parentCategory?.AddSubcategory(this);
        }

        public virtual void ClearParentCategory()
            => ParentCategory = null;
    }
}
