using BookWorld.Core.Repositories;
using BookWorld.Core.Services;
using Xunit;

namespace BookWorld.Tests;

public class UserAndBookServiceTests
{
    [Fact]
    public void RegisterAndUpdateUser_ShouldPersistChanges()
    {
        var repository = new InMemoryUserRepository();
        var service = new UserService(repository);

        var user = service.RegisterUser("Carlos Silva", "carlos@example.com", "+56912300000");
        service.UpdateUser(user.Id, "Carlos Silva", "carlos.nuevo@example.com", "+56912399999");

        var updatedUser = repository.GetById(user.Id);
        Assert.Equal("carlos.nuevo@example.com", updatedUser!.Email);
    }

    [Fact]
    public void RegisterAndUpdateBook_ShouldPersistChanges()
    {
        var repository = new InMemoryBookRepository();
        var service = new BookService(repository);

        var book = service.RegisterBook("Refactoring", "Martin Fowler", 1999, "Software");
        service.UpdateBook(book.Id, "Refactoring", "Martin Fowler", 2018, "Software");

        var updatedBook = repository.GetById(book.Id);
        Assert.Equal(2018, updatedBook!.PublicationYear);
    }
}
