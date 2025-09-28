namespace BookWorld.Core.Exceptions;

/// <summary>
/// Exception thrown when a book is requested for loan but is not currently available.
/// </summary>
public class BookUnavailableException : LibraryException
{
    public BookUnavailableException(string message) : base(message)
    {
    }
}
