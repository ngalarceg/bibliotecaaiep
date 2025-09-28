# Diseño orientado a objetos

## Arquitectura general

La solución se dividió en tres capas lógicas:

1. **Capa de dominio (`BookWorld.Core`)**: contiene entidades, excepciones, modelos de reporte, repositorios y servicios que encapsulan la lógica de negocio.
2. **Capa de presentación (`BookWorld.App`)**: aplicación de consola que orquesta los servicios y ofrece un menú interactivo para el bibliotecario.
3. **Capa de pruebas (`BookWorld.Tests`)**: proyecto xUnit que valida los escenarios críticos del dominio.

## Diagrama de clases UML

```plantuml
@startuml
class User {
  - Guid Id
  - string Name
  - string Email
  - string PhoneNumber
  + UpdateContactInformation(name, email, phone)
}

class Book {
  - Guid Id
  - string Title
  - string Author
  - int PublicationYear
  - string Genre
  - bool IsAvailable
  + UpdateMetadata(title, author, year, genre)
  + MarkAsBorrowed()
  + MarkAsReturned()
}

class Loan {
  - Guid Id
  - Guid UserId
  - Guid BookId
  - DateTime LoanDate
  - DateTime DueDate
  - DateTime? ReturnDate
  + RegisterReturn(returnDate)
}

interface IUserRepository
interface IBookRepository
interface ILoanRepository

class InMemoryUserRepository
class InMemoryBookRepository
class InMemoryLoanRepository

class UserService {
  - IUserRepository _userRepository
  + RegisterUser(...)
  + UpdateUser(...)
  + GetAllUsers()
}

class BookService {
  - IBookRepository _bookRepository
  + RegisterBook(...)
  + UpdateBook(...)
  + GetAllBooks()
}

class LoanService {
  - static LoanService _instance
  - IUserRepository _userRepository
  - IBookRepository _bookRepository
  - ILoanRepository _loanRepository
  + Initialize(...)
  + RegisterLoan(...)
  + ReturnLoan(...)
  + GetActiveLoans()
}

class ReportService {
  - IUserRepository _userRepository
  - IBookRepository _bookRepository
  - ILoanRepository _loanRepository
  + GetUsersWithActiveLoans()
}

UserService --> IUserRepository
BookService --> IBookRepository
LoanService --> IUserRepository
LoanService --> IBookRepository
LoanService --> ILoanRepository
ReportService --> IUserRepository
ReportService --> IBookRepository
ReportService --> ILoanRepository

InMemoryUserRepository ..|> IUserRepository
InMemoryBookRepository ..|> IBookRepository
InMemoryLoanRepository ..|> ILoanRepository
@enduml
```

## Modelo de datos

- **Usuarios**: identificados por `Guid`, se almacenan con nombre, correo y teléfono.
- **Libros**: identificados por `Guid`, mantienen título, autor, año de publicación, género y el estado `IsAvailable`.
- **Préstamos**: registran las relaciones `UserId` y `BookId`, junto a las fechas de préstamo, vencimiento y devolución.

## Flujos principales

1. **Registro de usuario/libro**: la capa de presentación captura los datos y delega al servicio correspondiente. El servicio valida y persiste mediante los repositorios en memoria.
2. **Préstamo**: el `LoanService` (Singleton) valida límites, verifica disponibilidad, crea el préstamo y actualiza el estado del libro.
3. **Devolución**: el `LoanService` ubica el préstamo activo, registra la devolución y marca el libro como disponible.
4. **Reporte**: el `ReportService` consolida información de usuarios, libros y préstamos para entregar una vista agregada.

## Consideraciones de extensibilidad

- Los repositorios en memoria pueden reemplazarse por implementaciones conectadas a bases de datos sin modificar los servicios.
- El menú de consola puede evolucionar a una interfaz gráfica reutilizando exactamente los mismos servicios.
- La clase `LoanService` expone `MaxActiveLoansPerUser`, permitiendo parametrizar fácilmente el límite de libros por usuario.
