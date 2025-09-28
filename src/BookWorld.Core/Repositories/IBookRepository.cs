using BookWorld.Core.Entities;

namespace BookWorld.Core.Repositories;

public interface IBookRepository
{
    Book Add(Book book);

    void Update(Book book);

    Book? GetById(Guid id);

    IEnumerable<Book> GetAll();
}
