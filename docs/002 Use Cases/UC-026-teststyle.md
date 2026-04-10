---
id: "026"
title: MSTest.Sdk-Style für Test-Projekte
tags:
  - UseCase
  - Querschnitt
status: open
---

# UC-026: MSTest.Sdk-Style für Test-Projekte

**User Story:** [US-031 Teststyle](../001%20User%20Stories/US-031-teststyle.md) | [Issue #31](https://github.com/RalfGuder/MCP-Booking/issues/31)

## Akteure

- **Primär:** Entwickler

## Vorbedingungen

1. Test-Projekte existieren mit dem bisherigen xUnit-Setup.
2. 10 bestehende Tests sind vorhanden und bestehen.

## Auslöser

Migration der Test-Projekte auf MSTest.Sdk-Style.

## Hauptablauf

1. Der Entwickler ändert das `Sdk`-Attribut in den Test-`.csproj`-Dateien zu `MSTest.Sdk/x.x.x`.
2. xUnit-spezifische NuGet-Pakete (xunit, xunit.runner.visualstudio, Microsoft.NET.Test.Sdk) werden entfernt.
3. `[Fact]` wird durch `[TestMethod]` ersetzt, Testklassen erhalten `[TestClass]`.
4. xUnit-spezifische Imports (`using Xunit;`) werden durch MSTest-Imports ersetzt.
5. Moq und Shouldly bleiben unverändert.
6. `dotnet test` wird ausgeführt — alle 10 Tests bestehen.

## Fehlerabläufe

### E1: Test schlägt nach Migration fehl
5a. Ein Test scheitert aufgrund von API-Unterschieden zwischen xUnit und MSTest.
6a. Der Entwickler passt den Test auf MSTest-Konventionen an.

## Nachbedingungen

- Alle Test-Projekte verwenden `Sdk="MSTest.Sdk"`.
- Alle 10 Tests bestehen.

## Test Case

- [TC-026](../004%20Test%20Cases/TC-026-teststyle.md)
