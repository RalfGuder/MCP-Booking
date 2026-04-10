---
id: "005"
title: Softwaredesign mit TDD
tags:
  - UserStory
status: open
---

# US-005: Softwaredesign mit TDD

**Issue:** [#5 — Softwaredesign](https://github.com/RalfGuder/MCP-Booking/issues/5)

## Story

**Als** Entwickler des MCP-Booking-Projekts,
**möchte ich** nach dem Test-Driven Development (TDD) Ansatz arbeiten,
**damit** die Codequalitaet durch umfassende Tests sichergestellt wird, Regressionen frühzeitig erkannt werden und das Design durch Tests getrieben emergent entsteht.

## Hintergrund

TDD folgt dem Red-Green-Refactor-Zyklus:

1. **Red:** Einen fehlschlagenden Test schreiben, der das gewünschte Verhalten beschreibt
2. **Green:** Den minimalen Code schreiben, damit der Test besteht
3. **Refactor:** Den Code verbessern, ohne das Verhalten zu ändern

In Kombination mit Clean Architecture (siehe [Issue #2](https://github.com/RalfGuder/MCP-Booking/issues/2)) ermöglicht TDD:
- Domain-Logik isoliert und schnell zu testen (Unit-Tests)
- Application-Use-Cases mit gemockten Repositories zu testen
- Infrastructure-Code gegen die echte API zu testen (Integrationstests)

## Akzeptanzkriterien

1. **Test-Framework & Tooling:**
   - Test-Framework: xUnit (Standard für .NET)
   - Mocking-Framework: Moq oder NSubstitute
   - Assertions: xUnit-eigene Assertions oder FluentAssertions
   - Alle Test-Projekte referenzieren `Microsoft.NET.Test.Sdk`, `xunit`, `xunit.runner.visualstudio`

2. **Test-Struktur:**
   - `McpBooking.Domain.Tests` — Unit-Tests für Entities, Value Objects, Domain-Logik
   - `McpBooking.Application.Tests` — Unit-Tests für Use Cases mit gemockten Repositories
   - `McpBooking.Infrastructure.Tests` — Integrationstests für HTTP-Client und Repository-Implementierungen
   - `McpBooking.Server.Tests` — Tests für MCP-Tool-Registrierung und -Ausführung

3. **TDD-Workflow:**
   - Jedes neue Feature beginnt mit einem fehlschlagenden Test
   - Implementierung erfolgt nur, um den Test zu bestehen
   - Nach dem Green-Schritt wird refactored
   - Tests sind benannt nach dem Muster: `MethodName_Scenario_ExpectedBehavior` oder `Should_ExpectedBehavior_When_Scenario`

4. **Testabdeckung:**
   - Alle öffentlichen Methoden der Domain-Schicht haben Unit-Tests
   - Alle Use Cases der Application-Schicht haben Unit-Tests
   - Jeder MCP-Tool-Endpunkt hat mindestens einen Happy-Path-Test und einen Error-Test
   - Edge Cases und Fehlerszenarien sind durch Tests abgedeckt

5. **Test-Ausführung:**
   - `dotnet test` führt alle Tests aus und meldet Erfolg
   - Tests sind unabhängig voneinander und können parallel laufen
   - Integrationstests sind durch `[Trait]` oder Kategorien von Unit-Tests trennbar

6. **Testbare Architektur:**
   - Dependency Injection ermöglicht das Ersetzen von Abhängigkeiten in Tests
   - Domain- und Application-Schicht haben keine statischen Abhängigkeiten
   - Interfaces statt konkreter Klassen für externe Abhängigkeiten

## Beispiel: TDD-Zyklus für eine Buchungsabfrage

```
// 1. RED — Test schreiben (schlägt fehl, da ListBookingsUseCase noch nicht existiert)
[Fact]
public async Task ListBookings_WithStatusFilter_ReturnsOnlyMatchingBookings()
{
    // Arrange
    var mockRepo = new Mock<IBookingRepository>();
    mockRepo.Setup(r => r.ListAsync(It.IsAny<BookingFilter>()))
            .ReturnsAsync(new List<Booking> { /* ... */ });
    var useCase = new ListBookingsUseCase(mockRepo.Object);

    // Act
    var result = await useCase.ExecuteAsync(new BookingFilter { Status = BookingStatus.Approved });

    // Assert
    Assert.All(result, b => Assert.Equal(BookingStatus.Approved, b.Status));
}

// 2. GREEN — Minimale Implementierung, die den Test bestehen lässt
// 3. REFACTOR — Code aufräumen, Duplikate entfernen
```

## Technische Hinweise

- Programmiersprache: C# (siehe [Issue #4](https://github.com/RalfGuder/MCP-Booking/issues/4))
- Architektur: Clean Architecture (siehe [Issue #2](https://github.com/RalfGuder/MCP-Booking/issues/2))
- Projektmappe: Visual Studio Solution (siehe [Issue #3](https://github.com/RalfGuder/MCP-Booking/issues/3))

## Abhängigkeiten

- Issue #3 (Neue Projektmappe) — Test-Projekte müssen in der Solution existieren
- Issue #4 (Programmiersprache) — bestimmt Test-Framework-Kompatibilität
