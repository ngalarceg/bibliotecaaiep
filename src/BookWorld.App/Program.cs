using BookWorld.Core.Repositories;
using BookWorld.Core.Services;

var userRepository = new InMemoryUserRepository();
var bookRepository = new InMemoryBookRepository();
var loanRepository = new InMemoryLoanRepository();

var userService = new UserService(userRepository);
var bookService = new BookService(bookRepository);
LoanService.Initialize(userRepository, bookRepository, loanRepository);
var loanService = LoanService.Instance;
var reportService = new ReportService(userRepository, bookRepository, loanRepository);

const int loanPeriodDays = 14;

Console.WriteLine("Bienvenido a BookWorld - Sistema de Gestión de Biblioteca");
string? option;
do
{
    Console.WriteLine();
    Console.WriteLine("Seleccione una opción:");
    Console.WriteLine("1. Registrar usuario");
    Console.WriteLine("2. Modificar usuario");
    Console.WriteLine("3. Listar usuarios");
    Console.WriteLine("4. Registrar libro");
    Console.WriteLine("5. Modificar libro");
    Console.WriteLine("6. Listar libros");
    Console.WriteLine("7. Registrar préstamo");
    Console.WriteLine("8. Registrar devolución");
    Console.WriteLine("9. Ver reporte de usuarios con préstamos");
    Console.WriteLine("0. Salir");
    Console.Write("Opción: ");
    option = Console.ReadLine();

    try
    {
        switch (option)
        {
            case "1":
                RegisterUser();
                break;
            case "2":
                UpdateUser();
                break;
            case "3":
                ListUsers();
                break;
            case "4":
                RegisterBook();
                break;
            case "5":
                UpdateBook();
                break;
            case "6":
                ListBooks();
                break;
            case "7":
                RegisterLoan();
                break;
            case "8":
                ReturnLoan();
                break;
            case "9":
                ShowLoanReport();
                break;
            case "0":
                Console.WriteLine("Hasta pronto!");
                break;
            default:
                Console.WriteLine("Opción no válida. Intente nuevamente.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
} while (option != "0");

void RegisterUser()
{
    Console.Write("Nombre: ");
    var name = Console.ReadLine() ?? string.Empty;
    Console.Write("Correo electrónico: ");
    var email = Console.ReadLine() ?? string.Empty;
    Console.Write("Teléfono: ");
    var phone = Console.ReadLine() ?? string.Empty;

    var user = userService.RegisterUser(name, email, phone);
    Console.WriteLine($"Usuario registrado con ID: {user.Id}");
}

void UpdateUser()
{
    Console.Write("ID del usuario: ");
    if (!Guid.TryParse(Console.ReadLine(), out var userId))
    {
        Console.WriteLine("ID inválido");
        return;
    }

    Console.Write("Nombre: ");
    var name = Console.ReadLine() ?? string.Empty;
    Console.Write("Correo electrónico: ");
    var email = Console.ReadLine() ?? string.Empty;
    Console.Write("Teléfono: ");
    var phone = Console.ReadLine() ?? string.Empty;

    userService.UpdateUser(userId, name, email, phone);
    Console.WriteLine("Usuario actualizado correctamente");
}

void ListUsers()
{
    var users = userService.GetAllUsers().ToList();
    if (!users.Any())
    {
        Console.WriteLine("No hay usuarios registrados.");
        return;
    }

    foreach (var user in users)
    {
        Console.WriteLine($"ID: {user.Id} | Nombre: {user.Name} | Email: {user.Email} | Teléfono: {user.PhoneNumber}");
    }
}

void RegisterBook()
{
    Console.Write("Título: ");
    var title = Console.ReadLine() ?? string.Empty;
    Console.Write("Autor: ");
    var author = Console.ReadLine() ?? string.Empty;
    Console.Write("Año de publicación: ");
    var publicationYearInput = Console.ReadLine();
    Console.Write("Género: ");
    var genre = Console.ReadLine() ?? string.Empty;

    if (!int.TryParse(publicationYearInput, out var year))
    {
        Console.WriteLine("Año inválido");
        return;
    }

    var book = bookService.RegisterBook(title, author, year, genre);
    Console.WriteLine($"Libro registrado con ID: {book.Id}");
}

void UpdateBook()
{
    Console.Write("ID del libro: ");
    if (!Guid.TryParse(Console.ReadLine(), out var bookId))
    {
        Console.WriteLine("ID inválido");
        return;
    }

    Console.Write("Título: ");
    var title = Console.ReadLine() ?? string.Empty;
    Console.Write("Autor: ");
    var author = Console.ReadLine() ?? string.Empty;
    Console.Write("Año de publicación: ");
    var publicationYearInput = Console.ReadLine();
    Console.Write("Género: ");
    var genre = Console.ReadLine() ?? string.Empty;

    if (!int.TryParse(publicationYearInput, out var year))
    {
        Console.WriteLine("Año inválido");
        return;
    }

    bookService.UpdateBook(bookId, title, author, year, genre);
    Console.WriteLine("Libro actualizado correctamente");
}

void ListBooks()
{
    var books = bookService.GetAllBooks().ToList();
    if (!books.Any())
    {
        Console.WriteLine("No hay libros registrados.");
        return;
    }

    foreach (var book in books)
    {
        var availability = book.IsAvailable ? "Disponible" : "Prestado";
        Console.WriteLine($"ID: {book.Id} | Título: {book.Title} | Autor: {book.Author} | Año: {book.PublicationYear} | Género: {book.Genre} | Estado: {availability}");
    }
}

void RegisterLoan()
{
    Console.Write("ID del usuario: ");
    if (!Guid.TryParse(Console.ReadLine(), out var userId))
    {
        Console.WriteLine("ID de usuario inválido");
        return;
    }

    Console.Write("ID del libro: ");
    if (!Guid.TryParse(Console.ReadLine(), out var bookId))
    {
        Console.WriteLine("ID de libro inválido");
        return;
    }

    var loan = loanService.RegisterLoan(userId, bookId, DateTime.Today, loanPeriodDays);
    Console.WriteLine($"Préstamo registrado. Fecha de devolución esperada: {loan.DueDate:d}");
}

void ReturnLoan()
{
    Console.Write("ID del libro: ");
    if (!Guid.TryParse(Console.ReadLine(), out var bookId))
    {
        Console.WriteLine("ID de libro inválido");
        return;
    }

    loanService.ReturnLoan(bookId, DateTime.Today);
    Console.WriteLine("Devolución registrada correctamente.");
}

void ShowLoanReport()
{
    var report = reportService.GetUsersWithActiveLoans().ToList();
    if (!report.Any())
    {
        Console.WriteLine("No existen préstamos activos.");
        return;
    }

    foreach (var item in report)
    {
        Console.WriteLine($"Usuario: {item.UserName} ({item.Email})");
        Console.WriteLine("Libros:");
        foreach (var book in item.BorrowedBooks)
        {
            Console.WriteLine($" - {book}");
        }
        Console.WriteLine();
    }
}
