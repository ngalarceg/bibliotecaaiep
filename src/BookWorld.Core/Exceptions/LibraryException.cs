namespace BookWorld.Core.Exceptions;

/// <summary>
/// Base exception for all custom library related errors.
/// </summary>
public class LibraryException : Exception
{
    public LibraryException(string message) : base(message)
    {
    }
}
