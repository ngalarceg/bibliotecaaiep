using BookWorld.Core.Entities;
using BookWorld.Core.Repositories;

namespace BookWorld.Core.Services;

/// <summary>
/// Provides operations for managing the lifecycle of books.
/// </summary>
public class BookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public Book RegisterBook(string title, string author, int publicationYear, string genre)
    {
        var book = new Book(title, author, publicationYear, genre);
        return _bookRepository.Add(book);
    }

    public void UpdateBook(Guid bookId, string title, string author, int publicationYear, string genre)
    {
        var book = _bookRepository.GetById(bookId) ?? throw new KeyNotFoundException($"Book with id {bookId} not found");
        book.UpdateMetadata(title, author, publicationYear, genre);
        _bookRepository.Update(book);
    }

    public IEnumerable<Book> GetAllBooks() => _bookRepository.GetAll();

    public Book? GetById(Guid bookId) => _bookRepository.GetById(bookId);
}
