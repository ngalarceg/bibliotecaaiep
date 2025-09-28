using BookWorld.Core.Entities;

namespace BookWorld.Core.Repositories;

public class InMemoryLoanRepository : ILoanRepository
{
    private readonly Dictionary<Guid, Loan> _loans = new();

    public Loan Add(Loan loan)
    {
        _loans[loan.Id] = loan;
        return loan;
    }

    public IEnumerable<Loan> GetAll() => _loans.Values.OrderByDescending(l => l.LoanDate);

    public Loan? GetActiveLoan(Guid bookId) => _loans.Values.FirstOrDefault(loan => loan.BookId == bookId && !loan.IsReturned);

    public IEnumerable<Loan> GetActiveLoansByUser(Guid userId) =>
        _loans.Values.Where(loan => loan.UserId == userId && !loan.IsReturned).OrderBy(l => l.DueDate);

    public void Update(Loan loan)
    {
        if (!_loans.ContainsKey(loan.Id))
        {
            throw new KeyNotFoundException($"Loan with id {loan.Id} was not found");
        }

        _loans[loan.Id] = loan;
    }
}
