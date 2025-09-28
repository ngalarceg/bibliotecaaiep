# Estrategia de pruebas y debugging

## Pruebas unitarias

El proyecto `BookWorld.Tests` utiliza **xUnit** y cubre los siguientes escenarios clave:

- `LoanServiceTests`
  - Registro de un préstamo cambia el estado del libro y calcula la fecha de devolución.
  - Validación del límite máximo de préstamos por usuario mediante la excepción `LoanLimitExceededException`.
  - Proceso de devolución que restablece la disponibilidad del libro y marca el préstamo como retornado.
- `ReportServiceTests`
  - Generación correcta del reporte consolidado de usuarios con préstamos activos.
- `UserAndBookServiceTests`
  - Verificación de que las operaciones de registro y actualización persisten los cambios en repositorios en memoria.

> **Ejecutar pruebas**
>
> ```bash
> dotnet test BookWorld.sln
> ```

## Debugging sugerido

1. Abrir la solución `BookWorld.sln` en Visual Studio 2022.
2. Establecer `BookWorld.App` como proyecto de inicio.
3. Colocar puntos de interrupción en los métodos de `LoanService` para observar el flujo de validaciones.
4. Utilizar el panel de **Autos** y **Watch** para inspeccionar las entidades `Book`, `User` y `Loan` durante el ciclo de vida del préstamo.

## Cobertura y futuras mejoras

- Al ejecutar `dotnet test` con la configuración existente se habilita `coverlet.collector`, lo que permite recopilar métricas de cobertura desde Visual Studio o la línea de comandos.
- Futuras iteraciones pueden incorporar pruebas de integración al reemplazar los repositorios en memoria por bases de datos reales.
