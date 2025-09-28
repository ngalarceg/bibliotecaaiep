# Requerimientos funcionales y no funcionales

## Requerimientos funcionales

1. **Gestión de usuarios**
   - Registrar usuarios capturando nombre completo, correo y teléfono.
   - Editar los datos de contacto de los usuarios.
   - Listar los usuarios registrados ordenados alfabéticamente.
2. **Gestión de libros**
   - Registrar libros con título, autor, año de publicación y género literario.
   - Actualizar la información de los libros existentes.
   - Listar el catálogo con el estado de disponibilidad en tiempo real.
3. **Préstamos de libros**
   - Permitir que un usuario retire hasta tres libros de manera simultánea.
   - Registrar la fecha del préstamo y calcular automáticamente la fecha esperada de devolución.
   - Registrar la devolución del libro liberando su disponibilidad.
4. **Reportes**
   - Mostrar un reporte con los usuarios que tienen préstamos activos y los títulos asociados.

## Requerimientos no funcionales

- **Plataforma:** aplicación de escritorio construida en .NET (C#) con soporte para Visual Studio 2022.
- **Arquitectura:** uso de programación orientada a objetos y capas lógicas (entidades, repositorios, servicios y aplicación).
- **Patrones de diseño:** aplicar el patrón Singleton para centralizar la lógica de préstamos y permitir futuras extensiones con otros patrones (por ejemplo repositorios).
- **Calidad:** contar con pruebas unitarias y soporte para debugging desde Visual Studio.
- **Documentación:** incluir guías técnicas y de usuario, junto con los diagramas de diseño.

## Casos de uso priorizados

| Prioridad | Caso de uso | Actores | Descripción resumida |
|-----------|-------------|---------|-----------------------|
| Alta | Registrar usuario | Bibliotecario | Capturar datos de un nuevo usuario, validando que los campos obligatorios estén completos. |
| Alta | Registrar libro | Bibliotecario | Ingresar un nuevo libro al catálogo con sus metadatos principales. |
| Alta | Registrar préstamo | Bibliotecario | Registrar el préstamo de un libro a un usuario, respetando el límite máximo de ejemplares. |
| Alta | Registrar devolución | Bibliotecario | Registrar la devolución de un libro y dejarlo disponible nuevamente. |
| Media | Modificar usuario | Bibliotecario | Actualizar correo o teléfono del usuario. |
| Media | Modificar libro | Bibliotecario | Ajustar los datos de un libro existente. |
| Media | Consultar reporte de préstamos | Bibliotecario | Visualizar los usuarios con préstamos activos y los títulos asociados. |

## Supuestos

- Los datos se almacenan en memoria, pero la capa de repositorios permite migrar a una base de datos en el futuro.
- La fecha de devolución esperada es fija (14 días) y puede cambiarse en configuración.
- Solo el personal autorizado utiliza la aplicación en un único equipo, por lo que la concurrencia se maneja a través del patrón Singleton.
