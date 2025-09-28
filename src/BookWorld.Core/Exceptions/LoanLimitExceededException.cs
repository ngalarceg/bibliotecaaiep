namespace BookWorld.Core.Exceptions;

/// <summary>
/// Exception thrown when a user attempts to borrow more books than the configured limit.
/// </summary>
public class LoanLimitExceededException : LibraryException
{
    public LoanLimitExceededException(string message) : base(message)
    {
    }
}
