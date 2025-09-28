using BookWorld.Core.Entities;
using BookWorld.Core.Exceptions;
using BookWorld.Core.Repositories;
using BookWorld.Core.Services;

using Xunit;

namespace BookWorld.Tests;

public class LoanServiceTests
{
    private readonly InMemoryUserRepository _userRepository = new();
    private readonly InMemoryBookRepository _bookRepository = new();
    private readonly InMemoryLoanRepository _loanRepository = new();

    private LoanService CreateService(int maxLoans = 3)
    {
        LoanService.ResetForTesting();
        return LoanService.Initialize(_userRepository, _bookRepository, _loanRepository, maxLoans);
    }

    private (User user, Book book) SeedUserAndBook()
    {
        var user = new User("Ana Pérez", "ana@example.com", "+56912345678");
        var book = new Book("Clean Code", "Robert C. Martin", 2008, "Software");
        _userRepository.Add(user);
        _bookRepository.Add(book);
        return (user, book);
    }

    [Fact]
    public void RegisterLoan_ShouldCreateLoanAndMarkBookAsUnavailable()
    {
        var service = CreateService();
        var (user, book) = SeedUserAndBook();

        var loan = service.RegisterLoan(user.Id, book.Id, new DateTime(2024, 5, 1), 7);

        Assert.False(_bookRepository.GetById(book.Id)!.IsAvailable);
        Assert.Equal(new DateTime(2024, 5, 8), loan.DueDate);
    }

    [Fact]
    public void RegisterLoan_ShouldThrowWhenUserExceedsLimit()
    {
        var service = CreateService(maxLoans: 1);
        var (user, book) = SeedUserAndBook();
        service.RegisterLoan(user.Id, book.Id, DateTime.Today, 7);

        var secondBook = new Book("The Pragmatic Programmer", "Andrew Hunt", 1999, "Software");
        _bookRepository.Add(secondBook);

        Assert.Throws<LoanLimitExceededException>(() => service.RegisterLoan(user.Id, secondBook.Id, DateTime.Today, 7));
    }

    [Fact]
    public void ReturnLoan_ShouldMarkLoanAndBookAsAvailable()
    {
        var service = CreateService();
        var (user, book) = SeedUserAndBook();
        service.RegisterLoan(user.Id, book.Id, DateTime.Today, 7);

        service.ReturnLoan(book.Id, DateTime.Today.AddDays(3));

        Assert.True(_bookRepository.GetById(book.Id)!.IsAvailable);
        Assert.True(_loanRepository.GetActiveLoansByUser(user.Id).All(loan => loan.IsReturned));
    }
}
