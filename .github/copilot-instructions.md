# Instrukcje – TaskFlow API

## Stack technologiczny
- .NET 9, C# 13
- ASP.NET Core Minimal API
- Entity Framework Core 9 + SQLite
- MediatR + FluentValidation + Ardalis.Result
- xUnit + FluentAssertions + NSubstitute

## Architektura – Clean Architecture
- `src/TaskFlow.Domain` – encje Project i TaskItem, interfejsy repozytoriów
- `src/TaskFlow.Application` – commandy, query, handlery MediatR, validatory
- `src/TaskFlow.Infrastructure` – EF Core DbContext, repozytoria
- `src/TaskFlow.API` – Minimal API endpoints, rejestracja DI

## Kluczowe konwencje
- Warstwa Application NIGDY nie używa DbContext bezpośrednio
- Każdy endpoint używa ISender (MediatR), nie serwisów bezpośrednio
- Handlery zwracają Ardalis.Result
- Async wszędzie – brak .Result i .Wait()
- CancellationToken jako ostatni parametr każdej metody async

## Nazewnictwo
- Commandy: [Akcja][Encja]Command (CreateProjectCommand)
- Query: Get[Encja]Query, Get[Encja]sQuery
- Handlery: [Nazwa]Handler
- Walidatory: [Nazwa]Validator
- Testy: [Klasa]Tests, metody: [Metoda]_When[Warunek]_Should[Wynik]

## Stack testowy
- xUnit 2, FluentAssertions, NSubstitute
- Wzorzec AAA z komentarzami // Arrange // Act // Assert
- Mockuj NSubstitute: _repo = Substitute.For()
- Testuj zachowanie (wynik), nie implementację (wywołania mocków)

## Czego NIE rób
- Nie dodawaj NuGet bez pytania
- Nie używaj DbContext w warstwie Application
- Nie hardcoduj connection strings
- Nie twórz kontrolerów MVC – tylko Minimal API