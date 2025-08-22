using System.Text.RegularExpressions;

namespace FinanceMath.Domain.Entities.Users
{
    public class Email
    {
        public string Value { get; private set; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be empty.");

            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Invalid email format.");

            Value = value.ToLowerInvariant();
        }

        public override string ToString() => Value;

        public override bool Equals(object obj) =>
            obj is Email email && Value == email.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
