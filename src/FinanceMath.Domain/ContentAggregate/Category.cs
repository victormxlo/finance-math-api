namespace FinanceMath.Domain.ContentAggregate
{
    public class Category
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual Category? ParentCategory { get; protected set; }
        public virtual IList<Category> SubCategories { get; protected set; } = new List<Category>();

        protected Category() { }

        public Category(string name, Category? parentCategory = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            ParentCategory = parentCategory;
        }

        public virtual void AddSubcategory(Category subCategory)
        {
            if (subCategory == null) throw new ArgumentNullException(nameof(subCategory));

            SubCategories.Add(subCategory);
        }
    }
}
