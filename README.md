# BookWorld

Sistema de escritorio (versión de consola) para gestionar usuarios, libros y préstamos de la biblioteca "BookWorld".

## Estructura del repositorio

- `BookWorld.sln`: solución principal para Visual Studio 2022.
- `src/BookWorld.Core`: biblioteca de clases con la lógica de dominio y servicios.
- `src/BookWorld.App`: aplicación de consola que implementa el flujo del bibliotecario.
- `tests/BookWorld.Tests`: pruebas unitarias con xUnit.
- `docs/`: documentación técnica (requerimientos, diseño UML, patrones, pruebas y manual de usuario/técnico).

## Requisitos

- SDK de .NET 8.0
- Visual Studio 2022 o Visual Studio Code con extensiones de C#

## Ejecución

```bash
dotnet build BookWorld.sln
dotnet run --project src/BookWorld.App/BookWorld.App.csproj
```

## Pruebas

```bash
dotnet test BookWorld.sln
```

## Documentación

Revisa la carpeta [`docs`](docs/) para encontrar los requerimientos, diagramas UML, patrones aplicados, estrategia de pruebas y manual de uso.
