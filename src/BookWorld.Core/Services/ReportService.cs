using BookWorld.Core.Models;
using BookWorld.Core.Repositories;

namespace BookWorld.Core.Services;

/// <summary>
/// Generates high-level reports for the library administrators.
/// </summary>
public class ReportService
{
    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;
    private readonly ILoanRepository _loanRepository;

    public ReportService(IUserRepository userRepository, IBookRepository bookRepository, ILoanRepository loanRepository)
    {
        _userRepository = userRepository;
        _bookRepository = bookRepository;
        _loanRepository = loanRepository;
    }

    /// <summary>
    /// Returns the list of users that currently have active loans along with the titles they borrowed.
    /// </summary>
    public IEnumerable<UserLoanReport> GetUsersWithActiveLoans()
    {
        var books = _bookRepository.GetAll().ToDictionary(book => book.Id, book => book.Title);
        var users = _userRepository.GetAll().ToDictionary(user => user.Id);

        var activeLoans = _loanRepository.GetAll().Where(loan => !loan.IsReturned);

        return activeLoans
            .GroupBy(loan => loan.UserId)
            .Select(group =>
            {
                var user = users[group.Key];
                var borrowedBooks = group.Select(loan => books.TryGetValue(loan.BookId, out var title) ? title : "Unknown Title")
                    .OrderBy(title => title)
                    .ToList();
                return new UserLoanReport(user.Id, user.Name, user.Email, borrowedBooks);
            })
            .OrderBy(report => report.UserName)
            .ToList();
    }
}
