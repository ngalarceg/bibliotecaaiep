using BookWorld.Core.Entities;
using BookWorld.Core.Exceptions;
using BookWorld.Core.Repositories;

namespace BookWorld.Core.Services;

/// <summary>
/// Handles the loan and return workflow of books. Implemented as a singleton to ensure a single source of truth.
/// </summary>
public sealed class LoanService
{
    private static readonly object SyncRoot = new();
    private static LoanService? _instance;

    public static LoanService Instance => _instance ?? throw new InvalidOperationException("LoanService has not been initialized");

    public static LoanService Initialize(IUserRepository userRepository, IBookRepository bookRepository, ILoanRepository loanRepository, int maxActiveLoansPerUser = 3)
    {
        if (userRepository is null)
        {
            throw new ArgumentNullException(nameof(userRepository));
        }

        if (bookRepository is null)
        {
            throw new ArgumentNullException(nameof(bookRepository));
        }

        if (loanRepository is null)
        {
            throw new ArgumentNullException(nameof(loanRepository));
        }

        lock (SyncRoot)
        {
            _instance ??= new LoanService(userRepository, bookRepository, loanRepository, maxActiveLoansPerUser);
            return _instance;
        }
    }

    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;
    private readonly ILoanRepository _loanRepository;

    public int MaxActiveLoansPerUser { get; }

    private LoanService(IUserRepository userRepository, IBookRepository bookRepository, ILoanRepository loanRepository, int maxActiveLoansPerUser)
    {
        if (maxActiveLoansPerUser <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxActiveLoansPerUser), "Loan limit must be greater than zero");
        }

        _userRepository = userRepository;
        _bookRepository = bookRepository;
        _loanRepository = loanRepository;
        MaxActiveLoansPerUser = maxActiveLoansPerUser;
    }

    public Loan RegisterLoan(Guid userId, Guid bookId, DateTime loanDate, int loanPeriodDays)
    {
        if (loanPeriodDays <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(loanPeriodDays), "Loan period must be at least one day");
        }

        var user = _userRepository.GetById(userId) ?? throw new KeyNotFoundException($"User with id {userId} was not found");
        var book = _bookRepository.GetById(bookId) ?? throw new KeyNotFoundException($"Book with id {bookId} was not found");

        var activeLoansForUser = _loanRepository.GetActiveLoansByUser(user.Id).ToList();
        if (activeLoansForUser.Count >= MaxActiveLoansPerUser)
        {
            throw new LoanLimitExceededException($"The user {user.Name} already has the maximum number of active loans ({MaxActiveLoansPerUser})");
        }

        if (!book.IsAvailable)
        {
            throw new BookUnavailableException($"The book '{book.Title}' is not currently available for loan");
        }

        var loan = new Loan(user.Id, book.Id, loanDate, loanDate.AddDays(loanPeriodDays));
        book.MarkAsBorrowed();
        _bookRepository.Update(book);
        return _loanRepository.Add(loan);
    }

    public Loan ReturnLoan(Guid bookId, DateTime returnDate)
    {
        var loan = _loanRepository.GetActiveLoan(bookId) ?? throw new KeyNotFoundException($"No active loan found for book id {bookId}");
        var book = _bookRepository.GetById(bookId) ?? throw new KeyNotFoundException($"Book with id {bookId} was not found");

        loan.RegisterReturn(returnDate);
        _loanRepository.Update(loan);

        book.MarkAsReturned();
        _bookRepository.Update(book);

        return loan;
    }

    public IEnumerable<Loan> GetActiveLoans() => _loanRepository.GetAll().Where(loan => !loan.IsReturned);

    public IEnumerable<Loan> GetAllLoans() => _loanRepository.GetAll();

    internal static void ResetForTesting()
    {
        lock (SyncRoot)
        {
            _instance = null;
        }
    }
}
