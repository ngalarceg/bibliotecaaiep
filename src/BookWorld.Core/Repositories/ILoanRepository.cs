using BookWorld.Core.Entities;

namespace BookWorld.Core.Repositories;

public interface ILoanRepository
{
    Loan Add(Loan loan);

    void Update(Loan loan);

    Loan? GetActiveLoan(Guid bookId);

    IEnumerable<Loan> GetActiveLoansByUser(Guid userId);

    IEnumerable<Loan> GetAll();
}
