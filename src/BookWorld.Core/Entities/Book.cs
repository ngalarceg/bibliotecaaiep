namespace BookWorld.Core.Entities;

/// <summary>
/// Represents a book that can be borrowed from the library.
/// </summary>
public class Book
{
    public Guid Id { get; }

    public string Title { get; private set; }

    public string Author { get; private set; }

    public int PublicationYear { get; private set; }

    public string Genre { get; private set; }

    public bool IsAvailable { get; private set; } = true;

    public Book(string title, string author, int publicationYear, string genre)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(author))
        {
            throw new ArgumentException("Author is required", nameof(author));
        }

        if (publicationYear <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(publicationYear), "Publication year must be positive");
        }

        if (string.IsNullOrWhiteSpace(genre))
        {
            throw new ArgumentException("Genre is required", nameof(genre));
        }

        Id = Guid.NewGuid();
        Title = title.Trim();
        Author = author.Trim();
        PublicationYear = publicationYear;
        Genre = genre.Trim();
    }

    public void UpdateMetadata(string title, string author, int publicationYear, string genre)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(author))
        {
            throw new ArgumentException("Author is required", nameof(author));
        }

        if (publicationYear <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(publicationYear), "Publication year must be positive");
        }

        if (string.IsNullOrWhiteSpace(genre))
        {
            throw new ArgumentException("Genre is required", nameof(genre));
        }

        Title = title.Trim();
        Author = author.Trim();
        PublicationYear = publicationYear;
        Genre = genre.Trim();
    }

    public void MarkAsBorrowed()
    {
        if (!IsAvailable)
        {
            throw new InvalidOperationException("Book is already borrowed");
        }

        IsAvailable = false;
    }

    public void MarkAsReturned()
    {
        IsAvailable = true;
    }
}
