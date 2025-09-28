namespace BookWorld.Core.Entities;

/// <summary>
/// Represents a registered user of the BookWorld library.
/// </summary>
public class User
{
    public Guid Id { get; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public string PhoneNumber { get; private set; }

    public User(string name, string email, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("User name is required", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is required", nameof(email));
        }

        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new ArgumentException("Phone number is required", nameof(phoneNumber));
        }

        Id = Guid.NewGuid();
        Name = name.Trim();
        Email = email.Trim();
        PhoneNumber = phoneNumber.Trim();
    }

    public void UpdateContactInformation(string name, string email, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("User name is required", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is required", nameof(email));
        }

        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new ArgumentException("Phone number is required", nameof(phoneNumber));
        }

        Name = name.Trim();
        Email = email.Trim();
        PhoneNumber = phoneNumber.Trim();
    }
}
