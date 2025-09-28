# Patrones de diseño aplicados

## Singleton en `LoanService`

El patrón **Singleton** garantiza que exista una única instancia de `LoanService`, responsable de coordinar los préstamos. De esta forma se centraliza la lógica de negocio crítica y se evita la duplicación de estados en memoria.

**Implementación clave:**

```csharp
public sealed class LoanService
{
    private static readonly object SyncRoot = new();
    private static LoanService? _instance;

    public static LoanService Initialize(IUserRepository userRepository, IBookRepository bookRepository, ILoanRepository loanRepository, int maxActiveLoansPerUser = 3)
    {
        lock (SyncRoot)
        {
            _instance ??= new LoanService(userRepository, bookRepository, loanRepository, maxActiveLoansPerUser);
            return _instance;
        }
    }

    public static LoanService Instance => _instance ?? throw new InvalidOperationException("LoanService has not been initialized");
}
```

- **Justificación:** La biblioteca requiere un único punto de control para validar el límite de préstamos por usuario, mantener la consistencia del inventario y generar reportes coherentes.
- **Beneficio adicional:** el método `ResetForTesting` permite reconfigurar el Singleton durante las pruebas unitarias sin afectar el comportamiento en producción.

## Patrón Repositorio

Si bien se utilizan repositorios en memoria, se definieron interfaces (`IUserRepository`, `IBookRepository`, `ILoanRepository`) que aíslan la lógica de persistencia. Esta decisión sigue el patrón **Repositorio**, facilitando reemplazar la infraestructura por bases de datos relacionales o NoSQL en el futuro.

## Buenas prácticas de POO

- **Encapsulamiento:** las entidades (`User`, `Book`, `Loan`) exponen métodos para modificar su estado, evitando cambios directos desde el exterior.
- **Inyección de dependencias:** los servicios reciben las implementaciones de repositorio como dependencias, fomentando el desacoplamiento y la testabilidad.
- **Modelos especializados:** la clase `UserLoanReport` encapsula la información que necesitan los reportes sin sobrecargar las entidades.
