using BookWorld.Core.Entities;

namespace BookWorld.Core.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    private readonly Dictionary<Guid, Book> _books = new();

    public Book Add(Book book)
    {
        _books[book.Id] = book;
        return book;
    }

    public IEnumerable<Book> GetAll() => _books.Values.OrderBy(b => b.Title);

    public Book? GetById(Guid id) => _books.TryGetValue(id, out var book) ? book : null;

    public void Update(Book book)
    {
        if (!_books.ContainsKey(book.Id))
        {
            throw new KeyNotFoundException($"Book with id {book.Id} was not found");
        }

        _books[book.Id] = book;
    }
}
