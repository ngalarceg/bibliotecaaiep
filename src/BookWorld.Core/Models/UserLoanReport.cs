namespace BookWorld.Core.Models;

/// <summary>
/// Represents the aggregation of loans per user for reporting purposes.
/// </summary>
public record UserLoanReport(Guid UserId, string UserName, string Email, IReadOnlyCollection<string> BorrowedBooks);
