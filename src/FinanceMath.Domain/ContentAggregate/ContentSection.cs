namespace FinanceMath.Domain.ContentAggregate
{
    public class ContentSection
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Title { get; protected set; }
        public virtual string Body { get; protected set; }
        public virtual int Order { get; protected set; }
        public virtual Content Content { get; protected set; }

        protected ContentSection() { }

        public ContentSection(string title, string body, int order, Content content)
        {
            Id = Guid.NewGuid();
            Title = title;
            Body = body;
            Order = order;
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public virtual void Update(string title, string body, int order)
        {
            if (!string.IsNullOrEmpty(title))
                Title = title;

            if (!string.IsNullOrEmpty(body))
                Body = body;

            if (order != default && order != Order)
                Order = order;

        }
    }
}
