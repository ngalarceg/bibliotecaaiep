using BookWorld.Core.Entities;
using BookWorld.Core.Repositories;

namespace BookWorld.Core.Services;

/// <summary>
/// Provides operations to manage library users.
/// </summary>
public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User RegisterUser(string name, string email, string phoneNumber)
    {
        var user = new User(name, email, phoneNumber);
        return _userRepository.Add(user);
    }

    public void UpdateUser(Guid userId, string name, string email, string phoneNumber)
    {
        var user = _userRepository.GetById(userId) ?? throw new KeyNotFoundException($"User with id {userId} not found");
        user.UpdateContactInformation(name, email, phoneNumber);
        _userRepository.Update(user);
    }

    public IEnumerable<User> GetAllUsers() => _userRepository.GetAll();

    public User? GetById(Guid userId) => _userRepository.GetById(userId);
}
