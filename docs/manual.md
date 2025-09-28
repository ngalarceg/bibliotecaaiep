# Manual técnico y de usuario

## Introducción

BookWorld es un sistema de escritorio basado en .NET que facilita la gestión de usuarios, libros y préstamos de la biblioteca "BookWorld". La versión incluida en este repositorio implementa una interfaz de consola pensada para ser ejecutada por el personal bibliotecario.

## Requisitos previos

- Windows 10/11 o cualquier sistema operativo compatible con .NET 8.0.
- SDK de .NET 8.0 y Visual Studio 2022 (o Visual Studio Code con extensiones de C#).

## Instrucciones de instalación

1. Clonar el repositorio: `git clone <url>`.
2. Abrir `BookWorld.sln` con Visual Studio 2022.
3. Restaurar paquetes y compilar la solución (Ctrl + Shift + B).

## Ejecución de la aplicación de escritorio

1. Establecer `BookWorld.App` como proyecto de inicio.
2. Ejecutar (F5) para iniciar el modo debugging o `Ctrl + F5` para ejecución sin depuración.
3. Se mostrará un menú interactivo en la consola.

## Guía de uso del menú

1. **Registrar usuario**: ingrese nombre, correo y teléfono. Se mostrará el identificador generado.
2. **Modificar usuario**: proporcione el ID y actualice los datos requeridos.
3. **Listar usuarios**: muestra todos los usuarios ordenados por nombre.
4. **Registrar libro**: ingrese título, autor, año de publicación y género.
5. **Modificar libro**: actualice los datos de un libro existente mediante su ID.
6. **Listar libros**: muestra el catálogo con estado `Disponible` o `Prestado`.
7. **Registrar préstamo**: introduzca el ID del usuario y del libro. El sistema valida el límite de préstamos y la disponibilidad.
8. **Registrar devolución**: indique el ID del libro prestado para devolverlo.
9. **Reporte de usuarios con préstamos**: muestra una lista de usuarios y los títulos que tienen actualmente prestados.
0. **Salir**: finaliza la aplicación.

## Estructura del código fuente

- `BookWorld.Core`: biblioteca de clases con la lógica de negocio.
  - `Entities`: modelos de dominio (`User`, `Book`, `Loan`).
  - `Repositories`: interfaces y repositorios en memoria.
  - `Services`: servicios de negocio (`UserService`, `BookService`, `LoanService`, `ReportService`).
  - `Models`: modelos de reporte (`UserLoanReport`).
  - `Exceptions`: excepciones personalizadas para reglas de negocio.
- `BookWorld.App`: aplicación de consola con el menú interactivo.
- `BookWorld.Tests`: proyecto xUnit con pruebas unitarias.
- `docs`: documentación asociada al proyecto (requerimientos, diseño, patrones, pruebas, manual).

## Mantenimiento y futuras mejoras

- Sustituir los repositorios en memoria por una capa de persistencia real (por ejemplo, Entity Framework Core).
- Agregar autenticación para controlar el acceso a la aplicación.
- Evolucionar la interfaz de consola a WPF o WinForms reutilizando la lógica existente.
- Integrar pipelines de CI para ejecutar las pruebas automáticamente en cada commit.
