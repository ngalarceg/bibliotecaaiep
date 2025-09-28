using BookWorld.Core.Entities;

namespace BookWorld.Core.Repositories;

public interface IUserRepository
{
    User Add(User user);

    void Update(User user);

    User? GetById(Guid id);

    IEnumerable<User> GetAll();
}
