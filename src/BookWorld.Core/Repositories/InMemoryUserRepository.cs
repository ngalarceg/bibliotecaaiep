using BookWorld.Core.Entities;

namespace BookWorld.Core.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<Guid, User> _users = new();

    public User Add(User user)
    {
        _users[user.Id] = user;
        return user;
    }

    public IEnumerable<User> GetAll() => _users.Values.OrderBy(u => u.Name);

    public User? GetById(Guid id) => _users.TryGetValue(id, out var user) ? user : null;

    public void Update(User user)
    {
        if (!_users.ContainsKey(user.Id))
        {
            throw new KeyNotFoundException($"User with id {user.Id} was not found");
        }

        _users[user.Id] = user;
    }
}
