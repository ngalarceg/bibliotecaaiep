using BookWorld.Core.Entities;
using BookWorld.Core.Models;
using BookWorld.Core.Repositories;
using BookWorld.Core.Services;
using Xunit;

namespace BookWorld.Tests;

public class ReportServiceTests
{
    [Fact]
    public void GetUsersWithActiveLoans_ShouldReturnAggregatedReport()
    {
        var userRepository = new InMemoryUserRepository();
        var bookRepository = new InMemoryBookRepository();
        var loanRepository = new InMemoryLoanRepository();

        var user = userRepository.Add(new User("María López", "maria@example.com", "+56977778888"));
        var firstBook = bookRepository.Add(new Book("Domain-Driven Design", "Eric Evans", 2003, "Software"));
        var secondBook = bookRepository.Add(new Book("Patterns of Enterprise Application Architecture", "Martin Fowler", 2002, "Software"));

        LoanService.ResetForTesting();
        var loanService = LoanService.Initialize(userRepository, bookRepository, loanRepository);
        loanService.RegisterLoan(user.Id, firstBook.Id, DateTime.Today, 7);
        loanService.RegisterLoan(user.Id, secondBook.Id, DateTime.Today, 7);

        var reportService = new ReportService(userRepository, bookRepository, loanRepository);
        var report = reportService.GetUsersWithActiveLoans().ToList();

        Assert.Single(report);
        UserLoanReport userReport = report.First();
        Assert.Equal(user.Name, userReport.UserName);
        Assert.Equal(2, userReport.BorrowedBooks.Count);
    }
}
