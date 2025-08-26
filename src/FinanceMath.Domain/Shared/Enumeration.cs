namespace FinanceMath.Domain.Shared
{
    public abstract class Enumeration : IComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
            => Name;

        public override bool Equals(object? obj)
        {
            if (obj is not Enumeration other) return false;

            return Id == other.Id && Name == other.Name;
        }

        public override int GetHashCode()
            => (Id, Name).GetHashCode();

        public int CompareTo(object? obj)
            => Id.CompareTo(((Enumeration)obj).Id);
    }
}
