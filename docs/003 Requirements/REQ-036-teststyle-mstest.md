---
id: "036"
title: MSTest.Sdk-Style
tags:
  - Requirement
  - Querschnitt
  - Technisch
priority: hoch
status: open
---

# REQ-036: MSTest.Sdk-Style

## Beschreibung

Alle Test-Projekte **müssen** den MSTest.Sdk-Style verwenden (`Sdk="MSTest.Sdk"`). xUnit wird durch MSTest-Attribute und -Konventionen ersetzt.

## Quelle

- [US-031](../001%20User%20Stories/US-031-teststyle.md) | [UC-026](../002%20Use%20Cases/UC-026-teststyle.md)

## Akzeptanzkriterien

1. Alle 4 Test-`.csproj`-Dateien verwenden `Sdk="MSTest.Sdk/x.x.x"`.
2. Keine xUnit-Abhängigkeiten mehr vorhanden (xunit, xunit.runner.visualstudio).
3. `[TestClass]` und `[TestMethod]` statt `[Fact]`.
4. Moq und Shouldly bleiben als Dependencies erhalten.
5. `dotnet test` führt alle Tests fehlerfrei aus.

## Testbarkeit

- Build-Test: `dotnet test` bestanden.
- Grep: Keine Referenzen auf `xunit` in `.csproj`-Dateien.
