namespace FinanceMath.Domain.Entities.Users
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string FullName { get; private set; }
        public Email Email { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; set; }

        public User(string username, string fullName, Email email, string passwordHash)
        {
            Id = Guid.NewGuid();
            Username = username ?? throw new ArgumentNullException(nameof(username));
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            CreatedAt = DateTime.Now;
        }

        public void UpdateUsername(string newUsername)
        {
            if (string.IsNullOrEmpty(newUsername))
                throw new ArgumentException("Username cannot be empty.");

            Username = newUsername;
            UpdatedAt = DateTime.Now;
        }
    }
}
