---
id: "026"
title: MSTest.Sdk-Style
tags:
  - TestCase
  - Querschnitt
status: ausstehend
---

# TC-026: MSTest.Sdk-Style

**Use Case:** [UC-026 MSTest.Sdk-Style](../002%20Use%20Cases/UC-026-teststyle.md)
**User Story:** [US-031 Teststyle](../001%20User%20Stories/US-031-teststyle.md)
**Requirement:** [REQ-036 MSTest.Sdk-Style](../003%20Requirements/REQ-036-teststyle-mstest.md)

## Testszenarien

### TS-026.01: Alle Test-Projekte verwenden MSTest.Sdk

**Schicht:** Build | **Status:** Ausstehend
**Vorbedingung:** Test-Projekte existieren.
**Aktion:** Grep nach `Sdk="MSTest.Sdk` in allen Test-`.csproj`-Dateien.
**Erwartetes Ergebnis:** 4 Treffer (alle Test-Projekte).

### TS-026.02: Keine xUnit-Abhängigkeiten vorhanden

**Schicht:** Build | **Status:** Ausstehend
**Vorbedingung:** Test-Projekte existieren.
**Aktion:** Grep nach `xunit` in allen `.csproj`-Dateien.
**Erwartetes Ergebnis:** 0 Treffer.

### TS-026.03: Alle Tests bestehen nach Migration

**Schicht:** Test | **Status:** Ausstehend
**Vorbedingung:** Migration auf MSTest abgeschlossen.
**Aktion:** `dotnet test` ausführen.
**Erwartetes Ergebnis:** Alle 10 Tests bestanden.

### TS-026.04: MSTest-Attribute korrekt verwendet

**Schicht:** Code-Analyse | **Status:** Ausstehend
**Vorbedingung:** Migration abgeschlossen.
**Aktion:** Grep nach `[TestClass]` und `[TestMethod]` in Test-Dateien.
**Erwartetes Ergebnis:** Alle Testklassen haben `[TestClass]`, alle Tests haben `[TestMethod]`.
