using FinanceMath.Domain.Users.Enums;

namespace FinanceMath.Domain.Users.Entities
{
    public class User
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Username { get; protected set; }
        public virtual string FullName { get; protected set; }
        public virtual Email Email { get; protected set; }
        public virtual string PasswordHash { get; protected set; }
        public virtual UserType Type { get; protected set; }
        public virtual DateTime CreatedAt { get; protected set; }
        public virtual DateTime? UpdatedAt { get; protected set; }

        protected User() { }

        public User(string username, string fullName, Email email, string passwordHash, UserType type)
        {
            Id = Guid.NewGuid();
            Username = username ?? throw new ArgumentNullException(nameof(username));
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            Type = type;
            CreatedAt = DateTime.Now;
        }

        public virtual void UpdateUsername(string newUsername)
        {
            if (string.IsNullOrEmpty(newUsername))
                throw new ArgumentException("Username cannot be empty.");

            Username = newUsername;
            UpdatedAt = DateTime.Now;
        }

        public virtual void ChangePassword(string passwordHash)
        {
            PasswordHash = passwordHash;
            UpdatedAt = DateTime.Now;
        }
    }
}
