---
id: "031"
title: Teststyle MSTest.Sdk
tags:
  - UserStory
  - Querschnitt
status: open
---

# US-031: Teststyle MSTest.Sdk

**Issue:** [#31 — US-031 Teststyle](https://github.com/RalfGuder/MCP-Booking/issues/31)

## Story

**Als** Entwickler des MCP-Booking-Projekts,
**möchte ich** dass alle Unit-Test-Projekte den MSTest.Sdk-Style verwenden,
**damit** die Test-Projekte den aktuellen Microsoft-Test-Standards entsprechen und einheitlich aufgebaut sind.

## Akzeptanzkriterien

- [ ] Alle Test-Projekte verwenden `Sdk="MSTest.Sdk"` statt `Sdk="Microsoft.NET.Sdk"`
- [ ] MSTest-Attribute (`[TestClass]`, `[TestMethod]`) ersetzen xUnit-Attribute (`[Fact]`)
- [ ] Bestehende 10 Tests sind auf MSTest migriert und bestehen
- [ ] `dotnet test` läuft fehlerfrei durch
- [ ] Moq und Shouldly bleiben als Mocking-/Assertion-Framework erhalten

## Technische Hinweise

- MSTest.Sdk-Style: `<Project Sdk="MSTest.Sdk/x.x.x">`
- Ersetzt: xUnit, xunit.runner.visualstudio, Microsoft.NET.Test.Sdk
- Betrifft alle 4 Test-Projekte

## Verknüpfte Artefakte

- Use Case: [UC-026-teststyle](../002%20Use%20Cases/UC-026-teststyle.md)
- Requirement: [REQ-036-teststyle-mstest](../003%20Requirements/REQ-036-teststyle-mstest.md)
- Test Case: [TC-026-teststyle](../004%20Test%20Cases/TC-026-teststyle.md)
