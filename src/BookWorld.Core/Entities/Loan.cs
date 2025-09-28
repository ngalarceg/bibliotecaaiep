namespace BookWorld.Core.Entities;

/// <summary>
/// Represents a loan transaction that keeps track of the borrowing lifecycle of a book.
/// </summary>
public class Loan
{
    public Guid Id { get; }

    public Guid UserId { get; }

    public Guid BookId { get; }

    public DateTime LoanDate { get; }

    public DateTime DueDate { get; }

    public DateTime? ReturnDate { get; private set; }

    public bool IsReturned => ReturnDate.HasValue;

    public Loan(Guid userId, Guid bookId, DateTime loanDate, DateTime dueDate)
    {
        if (loanDate > dueDate)
        {
            throw new ArgumentException("Loan date cannot be after due date", nameof(loanDate));
        }

        Id = Guid.NewGuid();
        UserId = userId;
        BookId = bookId;
        LoanDate = loanDate;
        DueDate = dueDate;
    }

    public void RegisterReturn(DateTime returnDate)
    {
        if (returnDate < LoanDate)
        {
            throw new ArgumentException("Return date cannot be before the loan date", nameof(returnDate));
        }

        ReturnDate = returnDate;
    }
}
